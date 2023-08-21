import React from "react";
import { LoadingOutlined } from "@ant-design/icons";
import { Button } from "reactstrap";
const LoadingButton = ({ externalClass, style, btnText, color }) => {
  return (
    <>
      <Button
        type="submit"
        className={`${externalClass}`}
        style={style}
        color={color}
      >
        <LoadingOutlined
          style={{
            fontSize: 24,
            marginRight: "5px",
          }}
          spin
        />
        {btnText}
      </Button>
    </>
  );
};

export default LoadingButton;
