import React from "react";
import { Input, Label } from "reactstrap";

const B2BLabelInput = ({
  id,
  name,
  type,
  classNameInput,
  classNameLabel,
  defaultChecked,
  labelName,
  onChangeHandller,
  role,
  value,
  invalid,
  defaultValue,
  disabled,
  required,
}) => {
  return (
    <>
      <Label className={classNameLabel} htmlFor={id}>
        {labelName} {required ? <span className="text-danger">*</span> : ""}
      </Label>
      <Input
        className={classNameInput}
        type={type}
        name={name}
        id={id}
        onChange={onChangeHandller}
        defaultChecked={defaultChecked}
        role={role}
        value={value}
        defaultValue={defaultValue}
        invalid={invalid}
        disabled={disabled}
      />
    </>
  );
};

export default B2BLabelInput;
