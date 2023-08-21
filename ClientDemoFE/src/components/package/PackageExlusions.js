import React from "react";
import { useState } from "react";
import { Col, Input, Tooltip } from "reactstrap";
import ButttonTravelNinjaz from "../Common/GloablMaster/ButttonTravelNinjaz";

const PackageExclusions = ({
  handleAddInclusions,
  inclusions,
  onChangeHandller,
  showInvalid,
  removeInclustionsHandller,
  toggleArrowTab,
  activeArrowTab,
  modalSaveHandller,
  cancelHandller,
  type,
}) => {
  const [tooltipOpenInclusion, setToolTipOpenInclusion] = useState(false);
  return (
    <>
      <Col xxl={12} className="cardItems">
        <div>
          <label htmlFor="add_city" className="form-label">
            Add Package Exclusions{" "}
            <i
              className="ri-add-line align-bottom mx-2"
              onClick={handleAddInclusions}
              id="package_inclusions"
              style={{
                padding: "3px",
                // marginTop: "10px",
                fontSize: "14px",
                borderRadius: "50%",
                backgroundColor: "#099885",
                color: "white",
                cursor: "pointer",
              }}
            ></i>
            <Tooltip
              isOpen={tooltipOpenInclusion}
              placement="right"
              target="package_inclusions"
              toggle={() => {
                setToolTipOpenInclusion(!tooltipOpenInclusion);
              }}
            >
              Add Package Exclusions
            </Tooltip>
          </label>
        </div>
        <div className="m-3">
          <div className="table-responsive table-card">
            <table
              className="table align-middle table-nowrap"
              id="customerTable"
            >
              <thead className="table-light">
                <tr>
                  <th className="">S.No.</th>
                  <th className="">Exclusions</th>
                  <th className="">Remove</th>
                </tr>
              </thead>
              <tbody>
                {inclusions.length > 0 &&
                  inclusions.map((item, index) => {
                    return (
                      <tr key={index}>
                        <td width={70}> {index + 1}</td>
                        <td width={850}>
                          <Input
                            type="text"
                            className="form-control"
                            name="inclusions"
                            id={item.id}
                            value={item.inclusions}
                            defaultValue={item.inclusions}
                            onChange={(e) => onChangeHandller(e)}
                            invalid={
                              (!item.inclusions ||
                                item.inclusions.length < 1) &&
                              showInvalid
                            }
                            // readOnly={readOnly}
                            required
                          />
                          {(!item.inclusions || item.inclusions.length < 1) &&
                          showInvalid ? (
                            <p
                              style={{
                                fontSize: "12px",
                                color: "red",
                              }}
                            >
                              Exclusion is Required
                            </p>
                          ) : (
                            ""
                          )}
                        </td>
                        <td width={100}>
                          <i
                            className="ri-close-line"
                            onClick={() => removeInclustionsHandller(item.id)}
                            style={{
                              fontSize: "25px",
                              cursor: "pointer",
                            }}
                          ></i>
                        </td>
                      </tr>
                    );
                  })}
              </tbody>
            </table>
          </div>
        </div>
      </Col>
      <div className="d-flex justify-content-xl-between">
        <div>
          <button
            type="button"
            className="btn btn-light btn-label previestab"
            onClick={() => {
              toggleArrowTab(activeArrowTab - 1);
            }}
          >
            <i className="ri-arrow-left-line label-icon align-middle fs-16 me-2"></i>{" "}
            Back
          </button>
        </div>
        <div>
          <ButttonTravelNinjaz
            backGroundColor={"primary"}
            onCLickHancller={modalSaveHandller}
            buttonText={"Save"}
            className="mx-1"
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

export default PackageExclusions;
