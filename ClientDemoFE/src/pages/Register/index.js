import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import {
  Card,
  Col,
  Container,
  Row,
  Form,
  FormFeedback,
  Input,
  Button,
} from "reactstrap";
import AuthSlider from "../AuthCarousel";
//formik
import { useFormik } from "formik";
import * as Yup from "yup";
import { signup } from "../../services/User/userService";
import SignuporsigninErrorMessage from "../../common/SignuporsigninErrorMessage";
import LoadingButton from "../../common/LoadingButton";
import { ToastContainer } from "react-toastify";
const Signup = () => {
  document.title = "SignUp | TravelNinjazB2B";

  const [passwordShow, setPasswordShow] = useState(false);
  const [confirmpasswordShow, setConfirmPasswordShow] = useState(false);
  const [errorMessage, setErrorMessage] = useState(undefined);
  const [isLoading, setIsLoading] = useState(false);
  const navigate = useNavigate();

  const signupUser = async (user) => {
    setIsLoading(true);
    try {
      const response = await signup({
        Email: user.email,
        UserName: user.userName,
        Password: user.password,
        ConfirmPassword: user.confirmpassword,
        Role: "Standard",
        Operation: "ADD",
      });
      if (response?.status === "SUCCESS") {
        navigate("/registrationsuccess");
        setErrorMessage(undefined);
      } else {
        setErrorMessage(response?.message);
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
      userName: "",
      email: "",
      password: "",
      confirmpassword: "",
    },
    validationSchema: Yup.object({
      password: Yup.string()
        .min(8, "Password must be at least 8 characters")
        .matches(RegExp("(.*[a-z].*)"), "At least lowercase letter")
        .matches(RegExp("(.*[A-Z].*)"), "At least uppercase letter")
        .matches(RegExp("(.*[0-9].*)"), "At least one number")
        .required("Password is required"),
      confirmpassword: Yup.string()
        .oneOf(
          [Yup.ref("password"), null],
          "Password and Confirm Password are not matched"
        )
        .required("Confirm Password is required"),
      userName: Yup.string().min(3).required("Username is required"),
      email: Yup.string().email().required("Email Id is required"),
    }),
    onSubmit: (values) => {
      signupUser(values);
    },
  });

  return (
    <>
      <ToastContainer />
      <div className="auth-page-wrapper auth-bg-cover py-5 d-flex justify-content-center align-items-center min-vh-100">
        <div className="bg-overlay"></div>
        <div className="auth-page-content overflow-hidden pt-lg-5">
          <Container>
            <Row>
              <Col lg={12}>
                <Card className="overflow-hidden m-0">
                  <Row className="justify-content-center g-0">
                    <AuthSlider />

                    <Col lg={7}>
                      <div className="p-lg-5 p-4">
                        <div>
                          <h5 className="text-primary">Register Account</h5>
                          <p className="text-muted">
                            Get your Free TravelNinjaz B2B account now.
                          </p>
                        </div>

                        <div className="mt-4">
                          <Form
                            onSubmit={validation.handleSubmit}
                            className="needs-validation"
                            noValidate
                            action="index"
                          >
                            <div className="mb-3 w-100">
                              <label htmlFor="useremail" className="form-label">
                                Email Id <span className="text-danger">*</span>
                              </label>
                              <Input
                                type="email"
                                className="form-control"
                                id="useremail"
                                placeholder="Enter Email Id"
                                name="email"
                                value={validation.values.email}
                                onBlur={validation.handleBlur}
                                onChange={validation.handleChange}
                                invalid={
                                  validation.errors.email &&
                                  validation.touched.email
                                    ? true
                                    : false
                                }
                                required
                              />
                              {validation.errors.email &&
                              validation.touched.email ? (
                                <FormFeedback type="invalid">
                                  {validation.errors.email}
                                </FormFeedback>
                              ) : null}
                            </div>
                            <div className="mb-3 w-100">
                              <label htmlFor="userName" className="form-label">
                                Username <span className="text-danger">*</span>
                              </label>
                              <Input
                                type="text"
                                className="form-control"
                                id="userName"
                                placeholder="Enter username"
                                name="userName"
                                value={validation.values.userName}
                                onBlur={validation.handleBlur}
                                onChange={validation.handleChange}
                                invalid={
                                  validation.errors.userName &&
                                  validation.touched.userName
                                    ? true
                                    : false
                                }
                                required
                              />
                              {validation.errors.userName &&
                              validation.touched.userName ? (
                                <FormFeedback type="invalid">
                                  {validation.errors.userName}
                                </FormFeedback>
                              ) : null}
                            </div>

                            <div className="mb-3 w-100">
                              <label
                                className="form-label"
                                htmlFor="password-input"
                              >
                                Password
                                <span className="text-danger">*</span>
                              </label>
                              <div className="position-relative auth-pass-inputgroup">
                                <Input
                                  type={passwordShow ? "text" : "password"}
                                  className="form-control pe-5 password-input"
                                  placeholder="Enter Password"
                                  id="password-input"
                                  name="password"
                                  value={validation.values.password}
                                  onBlur={validation.handleBlur}
                                  onChange={validation.handleChange}
                                  invalid={
                                    validation.errors.password &&
                                    validation.touched.password
                                      ? true
                                      : false
                                  }
                                />
                                {validation.errors.password &&
                                validation.touched.password ? (
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
                            <div className="mb-3 w-100">
                              <label
                                className="form-label"
                                htmlFor="confirmpassword-input"
                              >
                                Confirm Password
                                <span className="text-danger mx-1">*</span>
                              </label>
                              <div className="position-relative auth-pass-inputgroup">
                                <Input
                                  type={
                                    confirmpasswordShow ? "text" : "password"
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
                                    setConfirmPasswordShow(!confirmpasswordShow)
                                  }
                                  className="position-absolute end-0 top-0 text-decoration-none text-muted password-addon"
                                  type="button"
                                  id="password-addon"
                                >
                                  <i className="ri-eye-fill align-middle"></i>
                                </Button>
                              </div>
                            </div>

                            <div
                              id="password-contain"
                              className="p-3 bg-light mb-2 rounded"
                            >
                              <h5 className="fs-13">Password must contain:</h5>
                              <p
                                id="pass-length"
                                className="invalid fs-12 mb-2"
                              >
                                Minimum <b>8 characters</b>
                              </p>
                              <p id="pass-lower" className="invalid fs-12 mb-2">
                                At <b>lowercase</b> letter (a-z)
                              </p>
                              <p id="pass-upper" className="invalid fs-12 mb-2">
                                At least <b>uppercase</b> letter (A-Z)
                              </p>
                              <p
                                id="pass-number"
                                className="invalid fs-12 mb-0"
                              >
                                A least <b>number</b> (0-9)
                              </p>
                            </div>

                            <div className="mt-4">
                              {isLoading ? (
                                <LoadingButton
                                  btnText={"Sign Up"}
                                  externalClass={"success w-100"}
                                  color={"success"}
                                />
                              ) : (
                                <button
                                  className="btn btn-success w-100"
                                  type="submit"
                                >
                                  Sign Up
                                </button>
                              )}
                            </div>
                            {errorMessage !== undefined ? (
                              <SignuporsigninErrorMessage
                                message={errorMessage}
                              />
                            ) : (
                              ""
                            )}
                          </Form>
                        </div>

                        <div className="mt-5 text-center">
                          <p className="mb-0">
                            Already have an account ?{" "}
                            <Link
                              to="/login"
                              className="fw-semibold text-primary text-decoration-underline"
                            >
                              Signin
                            </Link>
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
            <div className="row">
              <div className="col-lg-12">
                <div className="text-center">
                  <p className="mb-0">
                    {new Date().getFullYear()} TravelNinjaz B2B. with{" "}
                    <i className="mdi mdi-heart text-danger"></i> by <b>THE</b>
                    Digitalogic
                  </p>
                </div>
              </div>
            </div>
          </Container>
        </footer>
      </div>
    </>
  );
};

export default Signup;
