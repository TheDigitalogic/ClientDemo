import FeatherIcon from "feather-icons-react/build/FeatherIcon";
import React from "react";
import { useState } from "react";
import { Card, CardBody, Col, Input, Label, Row, Tooltip } from "reactstrap";
import ButttonTravelNinjaz from "../Common/GloablMaster/ButttonTravelNinjaz";

const PackageItinerary = ({
  handleAddItinerary,
  packageItinerary,
  onChangeHandller,
  showInvalid,
  removeItineraryHandller,
  toggleArrowTab,
  activeArrowTab,
  modalSaveHandller,
  cancelHandller,
}) => {
  const [tooltipOpenItinerary, setTooltipOpenItinearary] = useState(false);
  return (
    <>
      <div>
        <Row>
          <Col xxl={12} className="mb-3 cardItems">
            <div className="my-2">
              <label htmlFor="add_city" className="form-label">
                Add Itinerary{" "}
                <i
                  className="ri-add-line align-bottom mx-2"
                  onClick={handleAddItinerary}
                  id="package_itinerary"
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
                  isOpen={tooltipOpenItinerary}
                  placement="right"
                  target="package_itinerary"
                  toggle={() => {
                    setTooltipOpenItinearary(!tooltipOpenItinerary);
                  }}
                >
                  Add Itinerary
                </Tooltip>
              </label>
            </div>
            {packageItinerary?.map((item, index) => {
              return (
                <Card key={index}>
                  <CardBody>
                    <Row className="my-1">
                      <Col xxl={1}>
                        <Label className="form-label">Day</Label>{" "}
                        <Input
                          type="number"
                          defaultValue={item.day}
                          value={item.day}
                          id={item.id}
                          name="day"
                          className="form-control mx-1"
                          onChange={onChangeHandller}
                          invalid={!item.day && showInvalid}
                        />
                        {!item.day && showInvalid ? (
                          <p
                            style={{
                              fontSize: "12px",
                              color: "red",
                            }}
                          >
                            Day is Required
                          </p>
                        ) : (
                          ""
                        )}
                      </Col>
                      <Col xxl={9}>
                        <Label className="form-label">Itinerary title</Label>
                        <Input
                          className="form-control"
                          id={item.id}
                          defaultValue={item.packageItineraryTitle}
                          value={item.packageItineraryTitle}
                          name="packageItineraryTitle"
                          onChange={onChangeHandller}
                          invalid={!item.packageItineraryTitle && showInvalid}
                        ></Input>
                        {!item.packageItineraryTitle && showInvalid ? (
                          <p
                            style={{
                              fontSize: "12px",
                              color: "red",
                            }}
                          >
                            Itinerary title is Required
                          </p>
                        ) : (
                          ""
                        )}
                      </Col>
                      <Col xxl={2}>
                        <FeatherIcon
                          icon="x"
                          style={{
                            color: "#364574",
                            cursor: "pointer",
                            marginLeft: "90px",
                            // marginTop:"30px"
                          }}
                          onClick={() => removeItineraryHandller(item.id)}
                        />
                      </Col>
                    </Row>
                    <Row className="my-1">
                      <Col xxl={12}>
                        <Label className="form-label">
                          Itinerary Description
                        </Label>
                        <textarea
                          className={
                            !item.packageItineraryDescription && showInvalid
                              ? "form-control border border-danger"
                              : "form-control"
                          }
                          value={item.packageItineraryDescription}
                          name="packageItineraryDescription"
                          defaultValue={item.packageItineraryDescription}
                          id={item.id}
                          onChange={onChangeHandller}
                          style={{ overflowY: "hidden" }}
                        ></textarea>
                        {!item.packageItineraryDescription && showInvalid ? (
                          <p
                            style={{
                              fontSize: "12px",
                              color: "red",
                            }}
                          >
                            Itinerary description is Required
                          </p>
                        ) : (
                          ""
                        )}
                      </Col>
                    </Row>
                  </CardBody>
                </Card>
              );
            })}
          </Col>
        </Row>
      </div>
      <div className="d-flex justify-content-xl-between my-3">
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

export default PackageItinerary;
