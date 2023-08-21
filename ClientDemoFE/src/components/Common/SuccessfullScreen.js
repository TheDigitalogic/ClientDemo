import React from "react";
import SuccessScreen from "../statusScreen/SuccesScreen";
const SuccessfullScreen = ({
  registrationStatus,
  description,
  linkText,
  link,
}) => {
  return (
    <>
      <SuccessScreen
        description={description}
        linkText={linkText}
        link={link}
        registrationStatus={registrationStatus}
        showButton={true}
      />
    </>
  );
};

export default SuccessfullScreen;
