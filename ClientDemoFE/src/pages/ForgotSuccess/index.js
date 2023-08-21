import React from "react";
import SuccessfullScreen from "../../components/Common/SuccessfullScreen";

const ForgotSuccess = () => {
  return (
    <>
      <SuccessfullScreen
        link={"/login"}
        registrationStatus={"Reset link sent successfully!"}
        description={"Please Check your email."}
        linkText={"Sign In"}
      />
    </>
  );
};

export default ForgotSuccess;
