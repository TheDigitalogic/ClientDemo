import React from "react";
import { Input } from "reactstrap";

const InputControll = ({
  labelName,
  name,
  readOnly,
  value,
  defaultValue,
  onChangeHandller,
  invalid,
  required,
  type,
  role,
  checked,
  defaultChecked,
  className,
}) => {
  return (
    <div>
      <label className="form-label">{labelName}</label>
      <Input
        type={type || "text"}
        className={className || "form-control"}
        name={name}
        role={role}
        value={value}
        defaultValue={defaultValue}
        onChange={(e) => onChangeHandller(e)}
        invalid={invalid}
        readOnly={readOnly}
        required={required}
        checked={checked}
        defaultChecked={defaultChecked}
      />
    </div>
  );
};

export default InputControll;
