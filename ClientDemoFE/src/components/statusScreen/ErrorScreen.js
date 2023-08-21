import React from "react";
import StatusScreen from "./StatusScreen";

const ErrorScreen = ({
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
        redText={true}
        registrationStatus={registrationStatus}
        showButton={showButton}
        isCheckBox={false}
      />
    </>
  );
};

export default ErrorScreen;
