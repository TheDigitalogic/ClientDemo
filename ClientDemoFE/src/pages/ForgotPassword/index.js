import React from "react";
import { Link, useNavigate } from "react-router-dom";
import {
  Button,
  Card,
  Col,
  Container,
  Row,
  Form,
  FormFeedback,
  Label,
  Input,
} from "reactstrap";

import AuthSlider from "../AuthCarousel";
import lottie from "lottie-web";
import { defineLordIconElement } from "lord-icon-element";
// register lottie and define custom element
//formik
import { useFormik } from "formik";
import * as Yup from "yup";
import { forgotPassword } from "../../services/User/userService";
import { useState } from "react";
import {
  errornotify,
  successNotify,
} from "../../components/Common/notification";
import LoadingButton from "../../common/LoadingButton";
import { appName } from "../../common/applicationName";
import { ToastContainer } from "react-toastify";

const ForgotPasswordScreen = () => {
  document.title = `${appName}-Forgot Password`;
  const [isLoading, setIsLoading] = useState(false);
  defineLordIconElement(lottie.loadAnimation);
  const navigate = useNavigate();
  const forgotPassowrdFunc = async (values) => {
    setIsLoading(true);
    try {
      const user = await forgotPassword({
        Email: values.email,
        Application_type: "admin",
      });
      if (user?.status === "SUCCESS") {
        navigate("/forgotsuccess");
        successNotify("Reset link sent successfully!");
      } else {
        throw user.message;
      }
    } catch (error) {
      errornotify(error);
    } finally {
      setIsLoading(false);
    }
  };
  const validation = useFormik({
    enableReinitialize: true,

    initialValues: {
      email: "",
    },
    validationSchema: Yup.object({
      email: Yup.string().required("Please Enter Your Email"),
    }),
    onSubmit: (values) => {
      forgotPassowrdFunc(values);
    },
  });

  return (
    <React.Fragment>
      <ToastContainer />
      <div className="auth-page-wrapper auth-bg-cover py-5 d-flex justify-content-center align-items-center min-vh-100">
        <div className="bg-overlay"></div>
        <div className="auth-page-content overflow-hidden pt-lg-5">
          <Container>
            <Row>
              <Col lg={12}>
                <Card className="overflow-hidden">
                  <Row className="justify-content-center g-0">
                    <AuthSlider />

                    <Col lg={7}>
                      <div className="p-lg-5 p-4">
                        <h5 className="text-primary">Forgot Password?</h5>
                        <p className="text-muted">
                          Reset password with TravelNinjaz B2B
                        </p>

                        <div className="mt-2 text-center">
                          <lord-icon
                            src="https://cdn.lordicon.com/rhvddzym.json"
                            trigger="loop"
                            colors="primary:#0ab39c"
                            className="avatar-xl"
                            style={{ width: "120px", height: "120px" }}
                          ></lord-icon>
                        </div>

                        <div
                          className="alert alert-borderless alert-warning text-center mb-2 mx-2"
                          role="alert"
                        >
                          Enter your email and instructions will be sent to you!
                        </div>
                        <div className="p-2">
                          <Form onSubmit={validation.handleSubmit}>
                            <div className="mb-4">
                              <Label className="form-label">Email</Label>
                              <Input
                                type="email"
                                className="form-control"
                                id="email"
                                placeholder="Enter email address"
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
                              />
                              {validation.errors.email &&
                              validation.touched.email ? (
                                <FormFeedback type="invalid">
                                  {validation.errors.email}
                                </FormFeedback>
                              ) : null}
                            </div>

                            <div className="text-center mt-4">
                              {isLoading ? (
                                <LoadingButton
                                  btnText={"Send Reset Link"}
                                  externalClass={"success w-100"}
                                  color={"success"}
                                />
                              ) : (
                                <Button
                                  color="success"
                                  className="w-100"
                                  type="submit"
                                >
                                  Send Reset Link
                                </Button>
                              )}
                            </div>
                          </Form>
                        </div>

                        <div className="mt-5 text-center">
                          <p className="mb-0">
                            Wait, I remember my password...{" "}
                            <Link
                              to="/login"
                              className="fw-bold text-primary text-decoration-underline"
                            >
                              {" "}
                              Click here{" "}
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
                    with <i className="mdi mdi-heart text-danger"></i> by
                    <b>THE</b>
                    Digitalogic
                  </p>
                </div>
              </Col>
            </Row>
          </Container>
        </footer>
      </div>
    </React.Fragment>
  );
};

export default ForgotPasswordScreen;
