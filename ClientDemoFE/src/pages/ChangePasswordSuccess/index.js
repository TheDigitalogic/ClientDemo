import React from "react";
import SuccessfullScreen from "../../components/Common/SuccessfullScreen";

const ChangePasswordSuccess = () => {
  return (
    <SuccessfullScreen
      link={"/login"}
      registrationStatus={"Password Changed Successfully !!"}
      description={"Now you can sign in."}
      linkText={"Sign In"}
    />
  );
};

export default ChangePasswordSuccess;
