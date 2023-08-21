import React from "react";
import { Col } from "reactstrap";
import Select from "react-select";
import ButttonTravelNinjaz from "../Common/GloablMaster/ButttonTravelNinjaz";
import CurrencyShow from "../../common/CurrencyShow";
const PackageTransportRate = ({
  transport,
  selectOptionHandller,
  showInvalid,
  transportOptions,
  animatedComponents,
  transportList,
  toggleArrowTab,
  activeArrowTab,
  modalSaveHandller,
  cancelHandller,
}) => {
  const currencySymbol = localStorage.getItem("currencySymbol");
  return (
    <>
      <div className="cardItems">
        <Col xxl={6} className="mb-3">
          <div>
            <label htmlFor="transport" className="form-label">
              Transport Rate
            </label>
            <Select
              value={transport}
              id="transport"
              name="transport"
              onChange={(chooseOption) => {
                selectOptionHandller(chooseOption, "transport");
              }}
              className={
                !transport && showInvalid ? "border border-danger" : ""
              }
              options={transportOptions}
              components={animatedComponents}
              // isDisabled={readOnly}
            />
            {!transport && showInvalid && (
              <p
                style={{
                  color: "red",
                  fontSize: "14px",
                  marginLeft: "5px",
                }}
              >
                {" "}
                Please select Transport
              </p>
            )}
          </div>
        </Col>
        <Col xxl={12} className="mb-3">
          {/**This is for details transport */}
          <label htmlFor="transportandvehicle" className="form-label">
            Transport And Price
          </label>
          <table className="table align-middle table-nowrap">
            <thead className="table-light">
              <tr>
                <th className="">S.No.</th>
                <th className="">Vehicle</th>
                <th className="">Price</th>
              </tr>
            </thead>
            <tbody>
              {transportList.length > 0 &&
                transport &&
                transportList
                  .filter((item) => {
                    return item.transport_rate_id === transport.value;
                  })[0]
                  ?.transportRateList.map((itemChild, indexChild) => {
                    return (
                      <tr key={indexChild}>
                        <td> {indexChild + 1} .</td>
                        <td>{itemChild.vehicle_name}</td>
                        <td>
                          <CurrencyShow
                            currency={itemChild.vehicle_price}
                            currencySymbol={currencySymbol}
                          />
                          {/* {parseInt(itemChild.vehicle_price).toLocaleString(
                            "en-IN",
                            {
                              maximumFractionDigits: 0,
                              style: "currency",
                              currency: "INR",
                            }
                          )} */}
                        </td>
                      </tr>
                    );
                  })}
            </tbody>
          </table>
        </Col>
      </div>
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

export default PackageTransportRate;
