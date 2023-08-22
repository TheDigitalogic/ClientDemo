import { WarningOutlined } from "@ant-design/icons";
import React from "react";
import lottie from "lottie-web";
import { defineLordIconElement } from "lord-icon-element";
const ConfirmationWarning = ({ message = "Are you sure to Cancel?" }) => {
  defineLordIconElement(lottie.loadAnimation);
  return (
    <>
      <div className="d-flex flex-column align-items-center justify-content-center">
        <lord-icon
          src="https://cdn.lordicon.com/tdrtiskw.json"
          trigger="loop"
          colors="primary:#121331,secondary:#08a88a"
          style={{ width: "120px", height: "120px" }}
        ></lord-icon>
        <p className="warningPara">{message}</p>
      </div>
    </>
  );
};

export default ConfirmationWarning;
