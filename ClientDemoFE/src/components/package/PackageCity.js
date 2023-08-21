import React, { useState } from "react";
import { Col, Input, Row, Tooltip } from "reactstrap";
import Select from "react-select";
import ButttonTravelNinjaz from "../Common/GloablMaster/ButttonTravelNinjaz";
import { DragDropContext, Draggable, Droppable } from "react-beautiful-dnd";
import dragIcon from "../../assets/images/drag.png";
const PackageCity = ({
  handleAddCity,
  cityAndNight,
  selectOptionHandller,
  showInvalid,
  cityOptions,
  animatedComponents,
  removeCityHandler,
  toggleArrowTab,
  activeArrowTab,
  modalSaveHandller,
  cancelHandller,
  onChangeHandller,
  handleDragEnd,
}) => {
  // Tooltip Open state
  const [tooltipOpen, setTooltipOpen] = React.useState(false);
  return (
    <>
      <div>
        <Row>
          <Col xxl={12} className="mb-3 cardItems">
            <div>
              <label htmlFor="add_city" className="form-label">
                Add City And Night{" "}
                <i
                  className="ri-add-line align-bottom mx-2"
                  onClick={handleAddCity}
                  id="add_city"
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
                  isOpen={tooltipOpen}
                  placement="right"
                  target="add_city"
                  toggle={() => {
                    setTooltipOpen(!tooltipOpen);
                  }}
                >
                  Add City
                </Tooltip>
              </label>
            </div>
            <div className="m-3">
              {/**This table for child */}
              <div
                className="table-responsive table-card"
                style={{ minHeight: "350px" }}
              >
                <DragDropContext onDragEnd={handleDragEnd}>
                  <table
                    className="table align-middle table-nowrap"
                    id="customerTable"
                  >
                    <thead className="table-light">
                      <tr>
                        <th></th>
                        <th className="">S.No.</th>
                        <th className="">City</th>
                        <th className="">Nights</th>
                        <th className="">Remove</th>
                      </tr>
                    </thead>
                    <Droppable droppableId="droppable-1">
                      {(provider) => (
                        <tbody
                          ref={provider.innerRef}
                          {...provider.droppableProps}
                        >
                          {cityAndNight.length > 0 &&
                            cityAndNight.map((item, index) => (
                              <Draggable
                                key={`dragableId-${index}`}
                                draggableId={`dragableId-${index}`}
                                index={index}
                                type="TASK"
                              >
                                {(provider) => (
                                  <tr
                                    key={index}
                                    {...provider.draggableProps}
                                    {...provider.dragHandleProps}
                                    ref={provider.innerRef}
                                  >
                                    <td
                                      {...provider.dragHandleProps}
                                      width={100}
                                    >
                                      <img
                                        src={dragIcon}
                                        alt="dragIcon"
                                        style={{
                                          height: "30px",
                                          width: "30px",
                                          opacity: "0.7",
                                        }}
                                      />
                                    </td>
                                    <td width={100}>City {index + 1}</td>
                                    <td width={520}>
                                      <Select
                                        value={item.city}
                                        id={item.id}
                                        name="city"
                                        onChange={(chooseOption) => {
                                          selectOptionHandller(
                                            chooseOption,
                                            "city",
                                            item.id
                                          );
                                        }}
                                        className={
                                          !item.city && showInvalid
                                            ? "border border-danger"
                                            : ""
                                        }
                                        key={index}
                                        options={cityOptions}
                                        components={animatedComponents}
                                        // isDisabled={readOnly}
                                      />
                                      {!item.city && showInvalid && (
                                        <p
                                          style={{
                                            color: "red",
                                            fontSize: "14px",
                                            marginLeft: "5px",
                                          }}
                                        >
                                          {" "}
                                          Please select City
                                        </p>
                                      )}
                                    </td>
                                    <td width={200}>
                                      <Input
                                        type="number"
                                        className="form-control"
                                        name="nights"
                                        id={item.id}
                                        value={item.nights}
                                        defaultValue={item.nights}
                                        onChange={(e) => onChangeHandller(e)}
                                        invalid={
                                          (item.nights?.length < 1 ||
                                            !item.nights) &&
                                          showInvalid
                                        }
                                        // readOnly={readOnly}
                                        required
                                      />
                                      {(item.nights?.length < 1 ||
                                        !item.nights) &&
                                      showInvalid ? (
                                        <p
                                          style={{
                                            fontSize: "12px",
                                            color: "red",
                                          }}
                                        >
                                          Nights is Required
                                        </p>
                                      ) : (
                                        ""
                                      )}
                                    </td>
                                    <td width={100}>
                                      <i
                                        className="ri-close-line"
                                        onClick={() =>
                                          removeCityHandler(item.id)
                                        }
                                        style={{
                                          fontSize: "25px",
                                          cursor: "pointer",
                                        }}
                                      ></i>
                                    </td>
                                  </tr>
                                )}
                              </Draggable>
                            ))}
                          {provider.placeholder}
                        </tbody>
                      )}
                    </Droppable>
                  </table>
                </DragDropContext>
              </div>
            </div>
          </Col>
        </Row>
      </div>
      <div className="d-flex align-items-start gap-3 mt-4">
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
        <div className="right ms-auto nexttab nexttab">
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

export default PackageCity;
