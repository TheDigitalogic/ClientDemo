import React from "react";
import "../assets/scss/components/signupsigninError.scss";
const SignuporsigninErrorMessage = ({ message }) => {
  return (
    <>
      <p className="errormessage">{message}</p>
    </>
  );
};

export default SignuporsigninErrorMessage;
