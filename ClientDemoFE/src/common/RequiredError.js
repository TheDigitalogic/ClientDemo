import React from "react";
const RequiredError = ({ errorMessage }) => {
  return (
    <>
      <p
        style={{
          fontSize: "12px",
          color: "red",
        }}
      >
        {errorMessage}
      </p>
    </>
  );
};

export default RequiredError;
