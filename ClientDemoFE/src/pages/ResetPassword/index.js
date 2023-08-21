import React, { useEffect } from "react";
import AuthCarousel from "../AuthCarousel";
import * as Yup from "yup";
import { useFormik } from "formik";
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
import { appName } from "../../common/applicationName";
import { useState } from "react";
import { Link, useNavigate, useSearchParams } from "react-router-dom";
import SignuporsigninErrorMessage from "../../common/SignuporsigninErrorMessage";
import { resetPassword } from "../../services/User/userService";
import LoadingButton from "../../common/LoadingButton";
const ResetPassword = () => {
  // document.title = `${appName}-Reset Password`;
  const [userLogin, setUserLogin] = useState([]);
  const [errorMessage, setErrorMessage] = useState(undefined);
  const [passwordShow, setPasswordShow] = useState(false);
  const [confirmPasswordShow, setConfirmPasswordShow] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [searchParams] = useSearchParams();
  const token = searchParams.get("t");
  const email = searchParams.get("e");
  const type = searchParams.get("type");
  const navigate = useNavigate();
  useEffect(() => {
    if (type === "changepassword") {
      document.title = `${appName}-Change Password`;
    } else {
      document.title = `${appName}-Reset Password`;
    }
  }, [type]);
  /**This function for user login */
  const resetPasswordFunction = async (values) => {
    setIsLoading(true);
    try {
      const response = await resetPassword({
        Email: values.username,
        Token: token,
        Password: values.password,
        ConfirmPassword: values.confirmpassword,
      });
      if (response?.status === "SUCCESS") {
        if (type === "changepassword") {
          navigate("/");
        } else {
          navigate("/passwordchangesuccess");
        }

        setErrorMessage(undefined);
      } else {
        throw response?.message;
      }
    } catch (error) {
      setErrorMessage(error);
    } finally {
      setIsLoading(false);
    }
  };
  const validation = useFormik({
    enableReinitialize: true,

    initialValues: {
      username: email || "",
      password: userLogin.password || "",
      confirmpassword: "",
    },
    validationSchema: Yup.object({
      username: Yup.string().email().required("Please Enter Your Username"),
      password: Yup.string().required("Please Enter Your Password"),
      confirmpassword: Yup.string()
        .oneOf(
          [Yup.ref("password"), null],
          "Password and Confirm Password are not matched"
        )
        .required("Confirm Password is required"),
    }),

    onSubmit: (values) => {
      resetPasswordFunction(values);
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
                    <AuthCarousel />
                    <Col lg={7}>
                      <div className="p-lg-5 p-4">
                        <div>
                          <h5 className="text-primary">
                            {type === "changepassword"
                              ? "Change Password!"
                              : "Reset Password!"}
                          </h5>
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
                                type="email"
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
                            <div className="mb-3">
                              <label
                                className="form-label"
                                htmlFor="confirmpassword-input"
                              >
                                Confirm Password
                              </label>
                              <div className="position-relative auth-pass-inputgroup">
                                <Input
                                  type={
                                    confirmPasswordShow ? "text" : "password"
                                  }
                                  className="form-control pe-5 password-input"
                                  placeholder="Enter Confirm Password"
                                  id="confirmpassword-input"
                                  name="confirmpassword"
                                  value={validation.values.confirmpassword}
                                  onBlur={validation.handleBlur}
                                  onChange={validation.handleChange}
                                  invalid={
                                    validation.errors.confirmpassword &&
                                    validation.touched.confirmpassword
                                      ? true
                                      : false
                                  }
                                />
                                {validation.errors.confirmpassword &&
                                validation.touched.confirmpassword ? (
                                  <FormFeedback type="invalid">
                                    {validation.errors.confirmpassword}
                                  </FormFeedback>
                                ) : null}
                                <Button
                                  color="link"
                                  onClick={() =>
                                    setConfirmPasswordShow(!confirmPasswordShow)
                                  }
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
                                  btnText={"Save Password"}
                                  externalClass={"w-100"}
                                  color={"success"}
                                />
                              ) : (
                                <Button
                                  color="success"
                                  className="w-100"
                                  type="submit"
                                >
                                  Save Password
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
                            Wait, I remember my password...
                            <Link
                              to="/login"
                              className="fw-semibold text-primary text-decoration-underline"
                            >
                              {" "}
                              Click here
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

export default ResetPassword;
