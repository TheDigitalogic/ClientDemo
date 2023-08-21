import React from "react";
import StatusScreen from "./StatusScreen";

const SuccessScreen = ({
  description,
  link,
  linkText,
  registrationStatus,
  showButton,
}) => {
  return (
    <>
      <StatusScreen
        description={description}
        link={link}
        linkText={linkText}
        redText={false}
        registrationStatus={registrationStatus}
        showButton={showButton}
        isCheckBox={true}
      />
    </>
  );
};

export default SuccessScreen;
