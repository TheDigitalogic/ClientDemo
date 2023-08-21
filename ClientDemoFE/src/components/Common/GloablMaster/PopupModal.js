import React from "react";
import {
  Col,
  Input,
  Label,
  Modal,
  ModalBody,
  ModalHeader,
  Tooltip,
} from "reactstrap";
import { destinationTypes } from "../../constants/destinationTypes";
import ButttonTravelNinjaz from "./ButttonTravelNinjaz";
import SearchableSelectOption from "./SearchableSelectOption";

const PopupModal = ({
  tog_scroll,
  headerName,
  isOpen,
  size,
  scrollable,
  handleAddCitySeeing,
  siteAndRateList,
  onChangeHandller,
  showInvalid,
  readOnly,
  removeSiteHandller,
  is_active,
  modalSaveHandller,
  cancelHandller,
  destinationOptions,
  cityOptions,
  destinationType,
  destination,
  onChange,
  city,
}) => {
  // Tooltip Open state
  const [tooltipOpen, setTooltipOpen] = React.useState(false);
  return (
    <>
      <Modal
        isOpen={isOpen}
        toggle={tog_scroll}
        centered
        size={size}
        scrollable={scrollable}
      >
        <ModalHeader className="bg-light p-3">{headerName}</ModalHeader>
        <ModalBody>
          <form>
            <div className="row g-3">
              <Col xxl={6}>
                <SearchableSelectOption
                  labelName={"Destination Type"}
                  options={destinationTypes}
                  value={destinationType}
                  isDisabled={readOnly}
                  onChange={onChange}
                  name={"destinationType"}
                  id={"destinationType"}
                />
              </Col>
              <Col xxl={6}>
                <SearchableSelectOption
                  labelName={"Destination"}
                  options={destinationOptions}
                  value={destination}
                  isDisabled={readOnly}
                  onChange={onChange}
                  name={"destination"}
                  id={"destination"}
                />
              </Col>
              <Col xxl={6}>
                <SearchableSelectOption
                  labelName={"City"}
                  options={cityOptions}
                  value={city}
                  isDisabled={readOnly}
                  onChange={onChange}
                  name={"city"}
                  id={"city"}
                  showInvalid={showInvalid}
                />
                {!city && showInvalid && (
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
              </Col>
              <hr />
              <Col xxl={12}>
                <div>
                  <label htmlFor="add_city" className="form-label">
                    Site Name And Rate{" "}
                    <i
                      className="ri-add-line align-bottom mx-2"
                      onClick={handleAddCitySeeing}
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
                      Add Site and Rate
                    </Tooltip>
                  </label>
                </div>
              </Col>
              <div>
                {/**Child Table */}
                <div className="table-responsive table-card">
                  <table
                    className="table align-middle table-nowrap"
                    id="customerTable"
                  >
                    <thead className="table-light">
                      <tr>
                        <th className="">S.No.</th>
                        <th className="">Site Name</th>
                        <th className="">Rate</th>
                        <th className="">Remove</th>
                      </tr>
                    </thead>
                    <tbody>
                      {" "}
                      {siteAndRateList?.map((item, index) => {
                        return (
                          <tr key={index}>
                            <td width={100}>{index + 1}</td>
                            <td width={350}>
                              <Input
                                type="text"
                                className="form-control"
                                name="site"
                                id={index + 1}
                                value={item.site}
                                defaultValue={item.site}
                                onChange={(e) => onChangeHandller(e)}
                                invalid={!item.site && showInvalid}
                                required
                              />
                              {!item.site && showInvalid ? (
                                <p style={{ fontSize: "12px", color: "red" }}>
                                  Site is Required
                                </p>
                              ) : (
                                ""
                              )}
                            </td>
                            <td width={200}>
                              <Input
                                type="number"
                                className="form-control"
                                name="rate"
                                id={index + 1}
                                value={item.rate}
                                defaultValue={item.rate}
                                onChange={(e) => onChangeHandller(e)}
                                invalid={!item.rate && showInvalid}
                                required
                              />
                              {!item.rate && showInvalid ? (
                                <p style={{ fontSize: "12px", color: "red" }}>
                                  Rate is Required
                                </p>
                              ) : (
                                ""
                              )}
                            </td>
                            <td>
                              <i
                                className="ri-close-line"
                                onClick={() => removeSiteHandller(item.key)}
                                style={{ fontSize: "25px", cursor: "pointer" }}
                              ></i>
                            </td>
                          </tr>
                        );
                      })}
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
            <div className="form-check form-switch form-switch-success my-3">
              <Input
                className="form-check-input"
                type="checkbox"
                role="switch"
                name="is_active"
                id="SwitchCheck3"
                onChange={(e) => onChangeHandller(e)}
                defaultChecked={is_active}
              />
              <Label className="form-check-label" htmlFor="SwitchCheck3">
                Is Active
              </Label>
            </div>
          </form>
        </ModalBody>
        <div className="modal-footer">
          <ButttonTravelNinjaz
            backGroundColor={"primary"}
            onCLickHancller={modalSaveHandller}
            buttonText={"Save"}
          />
          <ButttonTravelNinjaz
            backGroundColor={"danger"}
            onCLickHancller={cancelHandller}
            buttonText={"Cancel"}
          />
        </div>
      </Modal>
    </>
  );
};

export default PopupModal;
