/**Button For TravelNinjazB2B*/
import React from "react";
import { Button } from "reactstrap";

const ButttonTravelNinjaz = ({
  backGroundColor,
  className,
  id,
  onCLickHancller,
  buttonText,
  buttonIcon,
  type,
  style,
}) => {
  return (
    <>
      <Button
        color={backGroundColor}
        className={`${className}`}
        id={id}
        onClick={onCLickHancller}
        type={type || "submit"}
        style={style}
      >
        {buttonIcon}
        {buttonText}
      </Button>
    </>
  );
};

export default ButttonTravelNinjaz;
