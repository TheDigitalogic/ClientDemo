import React from "react";
import { Link } from "react-router-dom";
import { Card, Col, Container, Row } from "reactstrap";
import AuthCarousel from "../../pages/AuthCarousel";

const StatusScreen = ({
  registrationStatus,
  description,
  link,
  linkText,
  showButton,
  redText,
  isCheckBox,
}) => {
  return (
    <div className="auth-page-wrapper auth-bg-cover py-5 d-flex justify-content-center align-items-center min-vh-100">
      <div className="bg-overlay"></div>
      <div className="auth-page-content overflow-hidden pt-lg-5">
        <Container>
          <Row>
            <Col lg={12}>
              <Card className="overflow-hidden">
                <Row className="justify-content-center g-0">
                  <AuthCarousel />
                  <Col lg={7}>
                    <div className="p-lg-5 p-4 text-center">
                      <div className="avatar-lg mx-auto mt-2">
                        {isCheckBox ? (
                          <div className="avatar-title bg-light text-success display-3 rounded-circle">
                            <i className="ri-checkbox-circle-fill"></i>
                          </div>
                        ) : (
                          <div className="avatar-title bg-light text-danger display-3 rounded-circle">
                            <i className="ri-close-circle-fill"></i>
                          </div>
                        )}
                      </div>
                      <div className="mt-4 pt-2">
                        <h4 className={redText ? "text-danger" : ""}>
                          {registrationStatus}
                        </h4>
                        <p className="text-muted mx-4">{description}</p>
                        {showButton ? (
                          <div className="mt-4">
                            <Link to={link} className="btn btn-success w-100">
                              {linkText}
                            </Link>
                          </div>
                        ) : (
                          ""
                        )}
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
                  {new Date().getFullYear()} TravelNinjaz B2B. Crafted with{" "}
                  <i className="mdi mdi-heart text-danger"></i> by <b>THE</b>
                  Digitalogic
                </p>
              </div>
            </Col>
          </Row>
        </Container>
      </footer>
    </div>
  );
};

export default StatusScreen;
