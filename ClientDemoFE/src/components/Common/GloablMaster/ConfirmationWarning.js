import { WarningOutlined } from "@ant-design/icons";
import React from "react";
import lottie from "lottie-web";
import { defineLordIconElement } from "lord-icon-element";
const ConfirmationWarning = () => {
  defineLordIconElement(lottie.loadAnimation);
  return (
    <>
      <div className="warningContainer">
        <lord-icon
          src="https://cdn.lordicon.com/tdrtiskw.json"
          trigger="loop"
          colors="primary:#121331,secondary:#08a88a"
          style={{ width: "120px", height: "120px" }}
        ></lord-icon>
        <p className="warningPara">Are you sure to Cancel?</p>
      </div>
    </>
  );
};

export default ConfirmationWarning;
