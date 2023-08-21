import React from "react";
import { Col, Input, Label, Row } from "reactstrap";
import Select from "react-select";
import ButttonTravelNinjaz from "../Common/GloablMaster/ButttonTravelNinjaz";
import B2BLabelInput from "../Common/B2BLabelInput";
import RequiredError from "../../common/RequiredError";
const DestinationAndPackage = ({
  destinationType,
  selectOptionHandller,
  destinationTypes,
  animatedComponents,
  readOnly,
  destination,
  destinationOptions,
  package_name,
  nights,
  package_commision,
  showInvalid,
  packageDescription,
  onChangePackageHandller,
  modalSaveHandller,
  onChangeHandller,
  is_active,
  cancelHandller,
  specialCheckBox,
  fromDate,
  toDate,
  package_type,
  isInternational,
}) => {
  console.log("destinationType", destinationType);
  return (
    <>
      <div className="cardItems">
        <Row>
          <Col xxl={6} className="mb-3">
            <div>
              <label htmlFor="destinationType" className="form-label">
                Destination Type
              </label>
              <Select
                // value={destinationType}
                value={
                  isInternational
                    ? { label: "International", value: 2 }
                    : { label: "Domestic", value: 1 }
                }
                id="destinationType"
                name="destinationType"
                onChange={(chooseOption) => {
                  selectOptionHandller(chooseOption, "destinationType");
                }}
                defaultValue={destinationType}
                options={destinationTypes}
                components={animatedComponents}
                isDisabled={true}
              />
            </div>
          </Col>
          <Col lg={6} className="mb-3">
            <div>
              <label htmlFor="destination" className="form-label">
                Destination
              </label>
              <Select
                value={destination}
                id="destination"
                name="destination"
                onChange={(chooseOption) => {
                  selectOptionHandller(chooseOption, "destination");
                }}
                options={destinationOptions}
                components={animatedComponents}
                isDisabled={readOnly}
              />
            </div>
          </Col>
          <Col xxl={6} className="mb-3">
            {/**Package name */}
            <div>
              {/* <label htmlFor="package_name" className="form-label">
                Package Name
              </label>
              <Input
                type="text"
                className="form-control"
                name="package_name"
                value={package_name}
                defaultValue={package_name}
                onChange={(e) => onChangeHandller(e)}
                invalid={package_name?.length < 1 && showInvalid}
                required
              /> */}
              <B2BLabelInput
                classNameInput={"form-control"}
                classNameLabel={"form-label"}
                id={"package_name"}
                labelName={"Package Name"}
                value={package_name}
                defaultValue={package_name}
                invalid={package_name?.length < 1 && showInvalid}
                name="package_name"
                onChangeHandller={onChangeHandller}
                type="text"
              />
              {package_name?.length < 1 && showInvalid ? (
                <RequiredError errorMessage={" Package Name is Required"} />
              ) : (
                ""
              )}
            </div>
          </Col>
          <Col className="mb-3" xxl={6}>
            <div>
              <label htmlFor="package_commision" className="form-label">
                Package Commision
              </label>
              <div className="input-group">
                <Input
                  type="number"
                  className="form-control"
                  name="package_commision"
                  value={package_commision}
                  defaultValue={package_commision}
                  onChange={(e) => onChangeHandller(e)}
                  invalid={
                    (package_commision?.length < 1 || !package_commision) &&
                    showInvalid
                  }
                  // readOnly={readOnly}
                  required
                />
                <span className="input-group-text" id="basic-addon1">
                  %
                </span>
              </div>
              {(package_commision?.length < 1 || !package_commision) &&
              showInvalid ? (
                <p style={{ fontSize: "12px", color: "red" }}>
                  Package Commision is Required
                </p>
              ) : (
                ""
              )}
            </div>
          </Col>
          {isInternational ? (
            <Col xxl={6}>
              <B2BLabelInput
                classNameInput={"form-control"}
                classNameLabel={"form-label"}
                id={"1"}
                labelName={"Package Nights"}
                value={nights}
                defaultValue={nights}
                // invalid={package_name?.length < 1 && showInvalid}
                name="nights"
                onChangeHandller={onChangeHandller}
                type="number"
              />
            </Col>
          ) : (
            <></>
          )}
          <Col xxl={isInternational ? 6 : 12}>
            <div>
              <Label htmlFor="packageDescription" className="form-label">
                Description
              </Label>
              <textarea
                className={
                  !packageDescription && showInvalid
                    ? "border border-danger form-control descriptionPackage"
                    : "form-control descriptionPackage"
                }
                id="packageDescription"
                name="packageDescription"
                value={packageDescription}
                defaultValue={packageDescription}
                onChange={onChangePackageHandller}
                // rows="3"
              ></textarea>
              {!packageDescription && showInvalid && (
                <p
                  style={{
                    color: "red",
                    fontSize: "14px",
                    // marginLeft: "5px",
                  }}
                >
                  {" "}
                  Description is required!
                </p>
              )}
            </div>
          </Col>
          <Col className="mb-2 my-1" xxl={6}>
            <div className="d-xl-flex">
              <div className="form-check form-radio-success m-2">
                <B2BLabelInput
                  classNameInput="form-check-input"
                  classNameLabel="form-check-label"
                  defaultChecked={specialCheckBox.isbestselling}
                  id="isbestselling"
                  labelName="Is Best Selling"
                  name="isbestselling"
                  type="checkbox"
                  onChangeHandller={onChangeHandller}
                />
              </div>
              <div className="form-check form-radio-success m-2">
                <B2BLabelInput
                  classNameInput="form-check-input"
                  classNameLabel="form-check-label"
                  defaultChecked={specialCheckBox.ishoneymoonpackage}
                  id="ishoneymoonpackage"
                  labelName="Is Honeymoon Package"
                  name="ishoneymoonpackage"
                  type="checkbox"
                  onChangeHandller={onChangeHandller}
                />
              </div>
              <div className="form-check form-radio-success m-2">
                <B2BLabelInput
                  classNameInput="form-check-input"
                  classNameLabel="form-check-label"
                  defaultChecked={specialCheckBox.isfamilypackage}
                  id="isfamilypackage"
                  labelName="Is Family Package"
                  name="isfamilypackage"
                  type="checkbox"
                  onChangeHandller={onChangeHandller}
                />
              </div>
            </div>
            <div className="form-check form-switch form-switch-success my-3">
              <B2BLabelInput
                classNameInput="form-check-input"
                classNameLabel="form-check-label"
                defaultChecked={is_active}
                id="is_active"
                labelName="Is Active"
                name="is_active"
                type="checkbox"
                role={"switch"}
                onChangeHandller={onChangeHandller}
              />
            </div>
            {package_type === "fixedPackage" && (
              <Row className="my-3">
                <Label
                  htmlFor="valid"
                  className="form-label"
                  style={{ letterSpacing: "1px" }}
                >
                  Validity
                </Label>
                <Col xxl={6}>
                  <B2BLabelInput
                    classNameInput="form-control"
                    classNameLabel="form-label"
                    id="fromDate"
                    labelName="From"
                    name="fromDate"
                    type="date"
                    onChangeHandller={onChangeHandller}
                    value={fromDate}
                    invalid={!fromDate && showInvalid}
                  />
                  {!fromDate && showInvalid && (
                    <RequiredError errorMessage={"From Date is required !"} />
                  )}
                </Col>
                <Col xxl={6}>
                  <B2BLabelInput
                    classNameInput="form-control"
                    classNameLabel="form-label"
                    id="toDate"
                    labelName="To"
                    name="toDate"
                    type="date"
                    onChangeHandller={onChangeHandller}
                    value={toDate}
                    invalid={!toDate && showInvalid}
                  />
                  {!toDate && showInvalid && (
                    <RequiredError errorMessage={"To Date is required !"} />
                  )}
                </Col>
              </Row>
            )}
          </Col>
        </Row>
      </div>
      <div className="d-flex align-items-center gap-3 mt-4">
        <div className="right ms-auto nexttab nexttab">
          <ButttonTravelNinjaz
            backGroundColor={"primary"}
            onCLickHancller={modalSaveHandller}
            buttonText={"Save"}
            className="mx-1"
            type={"button"}
          />
          <ButttonTravelNinjaz
            backGroundColor={"danger"}
            onCLickHancller={cancelHandller}
            buttonText={"Cancel"}
            className="mx-1"
          />
        </div>
      </div>
    </>
  );
};

export default DestinationAndPackage;
