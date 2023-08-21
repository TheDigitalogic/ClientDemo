import React from "react";
import AuthSlider from "../AuthCarousel";
import SuccessfullScreen from "../../components/Common/SuccessfullScreen";
const RegistrationSuccess = () => {
  document.title =
    "Success Message | Velzon - React Admin & Dashboard Template";
  return (
    <>
      <SuccessfullScreen
        registrationStatus={"Registration Successfull !"}
        description={"Now you can sign in."}
        linkText={"Sign in"}
        link={"/login"}
      />
    </>
    // <React.Fragment>
    //   <div className="auth-page-wrapper auth-bg-cover py-5 d-flex justify-content-center align-items-center min-vh-100">
    //     <div className="bg-overlay"></div>
    //     <div className="auth-page-content overflow-hidden pt-lg-5">
    //       <Container>
    //         <Row>
    //           <Col lg={12}>
    //             <Card className="overflow-hidden">
    //               <Row className="justify-content-center g-0">
    //                 <AuthSlider />
    //                 <Col lg={7}>
    //                   <div className="p-lg-5 p-4 text-center">
    //                     <div className="avatar-lg mx-auto mt-2">
    //                       <div className="avatar-title bg-light text-success display-3 rounded-circle">
    //                         <i className="ri-checkbox-circle-fill"></i>
    //                       </div>
    //                     </div>
    //                     <div className="mt-4 pt-2">
    //                       <h4>Registration Successfull !</h4>
    //                       <p className="text-muted mx-4">
    //                         Now you can sign in.
    //                       </p>
    //                       <div className="mt-4">
    //                         <Link to="/login" className="btn btn-success w-100">
    //                           Sign in
    //                         </Link>
    //                       </div>
    //                     </div>
    //                   </div>
    //                 </Col>
    //               </Row>
    //             </Card>
    //           </Col>
    //         </Row>
    //       </Container>
    //     </div>

    //     <footer className="footer">
    //       <Container>
    //         <Row>
    //           <Col lg={12}>
    //             <div className="text-center">
    //               <p className="mb-0">
    //                 {new Date().getFullYear()} TravelNinjaz B2B. Crafted with{" "}
    //                 <i className="mdi mdi-heart text-danger"></i> by <b>THE</b>
    //                 Digitalogic
    //               </p>
    //             </div>
    //           </Col>
    //         </Row>
    //       </Container>
    //     </footer>
    //   </div>
    // </React.Fragment>
  );
};

export default RegistrationSuccess;
