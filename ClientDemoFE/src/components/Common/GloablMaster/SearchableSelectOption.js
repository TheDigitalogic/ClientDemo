import React from "react";
import Select from "react-select";
import makeAnimated from "react-select/animated";
const animatedComponents = makeAnimated();
const SearchableSelectOption = ({
  labelName,
  id,
  name,
  onChange,
  options,
  isDisabled,
  value,
  showInvalid,
}) => {
  return (
    <>
      <label htmlFor={id} className="form-label">
        {labelName}
      </label>
      <Select
        value={value}
        id={id}
        name={name}
        onChange={(chooseOption) => {
          onChange(chooseOption, name);
        }}
        options={options}
        components={animatedComponents}
        isDisabled={isDisabled}
        className={!value && showInvalid ? "border border-danger" : ""}
      />
    </>
  );
};

export default SearchableSelectOption;
