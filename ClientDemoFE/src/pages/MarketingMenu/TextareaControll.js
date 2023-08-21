import React from "react";
import { Label } from "reactstrap";

const TextareaControll = ({
  id,
  lableName,
  name,
  value,
  defaultValue,
  onChange,
}) => {
  return (
    <div>
      <Label htmlFor={id} className="form-label">
        {lableName}
      </Label>
      <textarea
        className={"form-control descriptionPackage"}
        id={id}
        name={name}
        value={value}
        defaultValue={defaultValue}
        onChange={onChange}
        // rows="3"
      ></textarea>
    </div>
  );
};

export default TextareaControll;
