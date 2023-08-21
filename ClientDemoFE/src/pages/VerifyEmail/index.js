import React from "react";
import { Link, useNavigate, useSearchParams } from "react-router-dom";
import { useState } from "react";
import { useEffect } from "react";
import { appName } from "../../common/applicationName";
import { userVerifyEmail } from "../../services/User/userService";
import SuccessfullScreen from "../../components/Common/SuccessfullScreen";
import ErrorScreen from "../../components/statusScreen/ErrorScreen";
import SuccessScreen from "../../components/statusScreen/SuccesScreen";
const VerifyEmail = () => {
  document.title = `${appName}-Email Verification`;
  const [searchParams] = useSearchParams();
  const token = searchParams.get("t");
  const email = searchParams.get("e");
  const [errorMessage, setErrorMessage] = useState(undefined);
  const [isLoading, setIsLoading] = useState(false);
  const [verificationSuccess, setVerificationSuccess] = useState(false);
  const navigate = useNavigate();
  /**resset password */
  const verifyEmail = async () => {
    setIsLoading(true);
    try {
      const response = await userVerifyEmail({
        Email: email,
        Token: token,
      });
      if (response.status === "SUCCESS") {
        setErrorMessage(undefined);
        setVerificationSuccess(true);
      } else {
        throw response.message;
      }
    } catch (error) {
      setErrorMessage(error);
      setVerificationSuccess(false);
    } finally {
      setIsLoading(false);
    }
  };
  useEffect(() => {
    if (token && email) {
      verifyEmail();
    }
  }, [token, email]);

  return (
    <>
      {verificationSuccess ? (
        <SuccessScreen
          description={"you are able to sign in."}
          link={"/login"}
          linkText={"Sign in"}
          registrationStatus={"Email Verified Successfully !"}
          showButton={true}
        />
      ) : (
        <ErrorScreen
          description={
            "If you experience any problem accessing the system, please contact our support team."
          }
          registrationStatus={"Email Verification Failed !"}
          showButton={false}
        />
      )}
    </>
  );
};

export default VerifyEmail;
