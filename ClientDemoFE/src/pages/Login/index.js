import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import {
  Card,
  Col,
  Container,
  Input,
  Label,
  Row,
  Button,
  FormFeedback,
} from "reactstrap";
import AuthSlider from "../AuthCarousel";
// Formik validation
import * as Yup from "yup";
import { useFormik } from "formik";
import {
  getUserAndCompany,
  login,
  loginWithSocial,
  saveCompany,
} from "../../services/User/userService";
import SignuporsigninErrorMessage from "../../common/SignuporsigninErrorMessage";
import { appName } from "../../common/applicationName";
import { useGoogleLogin } from "@react-oauth/google";
//import { ReactSession } from "react-client-session";
import { setLocalStorageItem, setSession } from "../../services/common/session";
import LoadingButton from "../../common/LoadingButton";
import FacebookLogin from "react-facebook-login/dist/facebook-login-render-props";
import { errornotify } from "../../components/Common/notification";
import axios from "axios";
import { faceBookAppId } from "../../services/common/const";
const CoverSignIn = () => {
  document.title = `${appName}-Login`;
  const [userLogin, setUserLogin] = useState([]);
  const [errorMessage, setErrorMessage] = useState(undefined);
  const [passwordShow, setPasswordShow] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const navigate = useNavigate();
  /**This function for user login */
  const loginUser = async (values) => {
    setIsLoading(true);
    try {
      // Check if User is valid from WebAPI Database
      const user = await login({
        Username: values?.username,
        Password: values?.password,
      });
      if (user?.status === "SUCCESS") {
        var promise = setSession("userDetails", user);
        promise.then(function () {
          navigate("/");
        });
        setErrorMessage(undefined);
      } else {
        setErrorMessage(user.message);
      }
    } catch (error) {
      setErrorMessage(error);
    } finally {
      setIsLoading(false);
    }
  };
  const validation = useFormik({
    // enableReinitialize : use this flag when initial values needs to be changed
    enableReinitialize: true,

    initialValues: {
      username: userLogin.username || "",
      password: userLogin.password || "",
    },
    validationSchema: Yup.object({
      username: Yup.string().required("Please Enter Your Username"),
      password: Yup.string().required("Please Enter Your Password"),
    }),

    onSubmit: (values) => {
      loginUser(values);
    },
  });

  return (
    <>
      <div className="auth-page-wrapper auth-bg-cover py-5 d-flex justify-content-center align-items-center min-vh-100">
        <div className="bg-overlay"></div>
        <div className="auth-page-content overflow-hidden pt-lg-5">
          <Container>
            <Row>
              <Col lg={12}>
                <Card className="overflow-hidden">
                  <Row className="g-0">
                    <AuthSlider />
                    <Col lg={7}>
                      <div className="p-lg-5 p-4">
                        <div>
                          <h5 className="text-primary">Welcome Back !</h5>
                          <p className="text-muted">
                            Sign in to continue to TravelNinjaz B2B.
                          </p>
                        </div>

                        <div className="mt-4">
                          <form
                            onSubmit={(e) => {
                              e.preventDefault();
                              validation.handleSubmit();
                              return false;
                            }}
                            action="#"
                          >
                            <div className="mb-3">
                              <Label htmlFor="username" className="form-label">
                                Username
                              </Label>
                              <Input
                                type="text"
                                name="username"
                                className="form-control"
                                id="username"
                                placeholder="Enter username"
                                onChange={validation.handleChange}
                                onBlur={validation.handleBlur}
                                value={validation.values.username || ""}
                                invalid={
                                  validation.touched.username &&
                                  validation.errors.username
                                    ? true
                                    : false
                                }
                              />
                              {validation.touched.username &&
                              validation.errors.username ? (
                                <FormFeedback type="invalid">
                                  {validation.errors.username}
                                </FormFeedback>
                              ) : null}
                            </div>

                            <div className="mb-3">
                              <Label
                                className="form-label"
                                htmlFor="password-input"
                              >
                                Password
                              </Label>
                              <div className="position-relative auth-pass-inputgroup mb-3">
                                <Input
                                  type={passwordShow ? "text" : "password"}
                                  name="password"
                                  className="form-control pe-5"
                                  placeholder="Enter password"
                                  id="password-input"
                                  value={validation.values.password || ""}
                                  onChange={validation.handleChange}
                                  onBlur={validation.handleBlur}
                                  invalid={
                                    validation.touched.password &&
                                    validation.errors.password
                                      ? true
                                      : false
                                  }
                                />
                                {validation.touched.password &&
                                validation.errors.password ? (
                                  <FormFeedback type="invalid">
                                    {validation.errors.password}
                                  </FormFeedback>
                                ) : null}
                                <Button
                                  color="link"
                                  onClick={() => setPasswordShow(!passwordShow)}
                                  className="position-absolute end-0 top-0 text-decoration-none text-muted password-addon"
                                  type="button"
                                  id="password-addon"
                                >
                                  <i className="ri-eye-fill align-middle"></i>
                                </Button>
                              </div>
                            </div>

                            <div className="mt-4">
                              {isLoading ? (
                                <LoadingButton
                                  btnText={"Sign In"}
                                  externalClass={"success w-100"}
                                  color={"success"}
                                />
                              ) : (
                                <Button
                                  color="success"
                                  className="w-100"
                                  type="submit"
                                >
                                  Sign In
                                </Button>
                              )}
                            </div>
                            {errorMessage !== undefined ? (
                              <SignuporsigninErrorMessage
                                message={errorMessage}
                              />
                            ) : (
                              ""
                            )}
                          </form>
                        </div>

                        <div className="mt-5 text-center">
                          <p className="mb-0">
                            Don't have an account ?{" "}
                            <Link
                              to="/signup"
                              className="fw-semibold text-primary text-decoration-underline"
                            >
                              {" "}
                              Signup
                            </Link>{" "}
                          </p>
                        </div>
                      </div>
                    </Col>
                  </Row>
                </Card>
              </Col>
            </Row>
          </Container>
        </div>

        <footer className="footer">
          <Container>
            <Row>
              <Col lg={12}>
                <div className="text-center">
                  <p className="mb-0">
                    &copy; {new Date().getFullYear()} TravelNinjaz B2B. Crafted
                    with <i className="mdi mdi-heart text-danger"></i> by &nbsp;
                    <b>THE</b>
                    Digitalogic
                  </p>
                </div>
              </Col>
            </Row>
          </Container>
        </footer>
      </div>
    </>
  );
};

export default CoverSignIn;
