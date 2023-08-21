<Modal
  // isOpen={modal_scroll}
  isOpen={false}
  toggle={() => {
    tog_scroll();
  }}
  centered
  size="lg"
  // scrollable={true}
  className="modal-fullscreen"
>
  <ModalHeader
    className="bg-light p-3"
    toggle={() => {
      tog_scroll();
    }}
  >
    {!readOnly ? "Add" : "Edit"} Package
  </ModalHeader>
  <ModalBody style={{ overflowX: "hidden" }}>
    <form>
      <div className="row g-3">
        <Col xxl={8}>
          <div>
            <label htmlFor="destinationType" className="form-label">
              Destination Type
            </label>
            <Select
              value={destinationType}
              id="destinationType"
              name="destinationType"
              onChange={(chooseOption) => {
                selectOptionHandller(chooseOption, "destinationType");
              }}
              options={destinationTypes}
              components={animatedComponents}
              isDisabled={readOnly}
            />
          </div>
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
          {/**Package name */}
          <div>
            <label htmlFor="package_name" className="form-label">
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
              // readOnly={readOnly}
              required
            />
            {package_name?.length < 1 && showInvalid ? (
              <p style={{ fontSize: "12px", color: "red" }}>
                Package Name is Required
              </p>
            ) : (
              ""
            )}
          </div>
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
        <Col xxl={4}>
          <div className="d-xl-flex">
            <div>
              <label htmlFor="imageuplod" className="form-label">
                Image upload
              </label>
              <div>
                <label
                  htmlFor="imageuplod"
                  className="form-label"
                  style={
                    showInvalid && !imagePreviewFileName
                      ? {
                          backgroundColor: "#F5F7FA",
                          color: "black",
                          padding: "9px",
                          borderRadius: "5px",
                          cursor: "pointer",
                          border: "1px solid red",
                        }
                      : {
                          backgroundColor: "#F5F7FA",
                          color: "black",
                          padding: "9px",
                          borderRadius: "5px",
                          cursor: "pointer",
                        }
                  }
                >
                  Choose image
                </label>
                {!imagePreviewFileName && showInvalid && (
                  <p style={{ fontSize: "12px", color: "red" }}>
                    Image is Required
                  </p>
                )}
                <input
                  type="file"
                  id="imageuplod"
                  name="file"
                  accept="image/*"
                  style={{ display: "none" }}
                  onChange={onChangeImageHandller}
                />
              </div>
            </div>
            {(imagePreviewFile || readOnly) && (
              <div>
                <Image
                  src={
                    readOnly && !imagePreviewFile
                      ? `${url}/Images/Package/${imagePreviewFileName}`
                      : imagePreviewFile
                  }
                  alt="Package"
                  width={70}
                  height={50}
                  style={{
                    marginLeft: "30px",
                    borderRadius: "5px",
                  }}
                ></Image>
                {imagePreviewFileName && (
                  <p
                    style={{
                      marginLeft: "30px",
                      marginTop: "5px",
                    }}
                  >
                    {imagePreviewFileName}
                  </p>
                )}
              </div>
            )}
          </div>
        </Col>
        <Col xxl={2}>
          <div className="form-check form-radio-success mb-3">
            <Label className="form-check-label" htmlFor="isbestselling">
              Is Best Selling
            </Label>
            <Input
              className="form-check-input"
              type="checkbox"
              name="isbestselling"
              id="isbestselling"
              onChange={onChangeHandller}
              defaultChecked={specialCheckBox.isbestselling}
            />
          </div>
        </Col>
        <Col xxl={2}>
          <div className="form-check form-radio-success mb-3">
            <Label className="form-check-label" htmlFor="ishoneymoonpackage">
              Is Honeymoon Package
            </Label>
            <Input
              className="form-check-input"
              type="checkbox"
              name="ishoneymoonpackage"
              id="ishoneymoonpackage"
              onChange={onChangeHandller}
              defaultChecked={specialCheckBox.ishoneymoonpackage}
            />
          </div>
        </Col>
        <Col xxl={2}>
          <div className="form-check form-radio-success mb-3">
            <Label className="form-check-label" htmlFor="isfamilypackage">
              Is Family Package
            </Label>
            <Input
              className="form-check-input"
              type="checkbox"
              name="isfamilypackage"
              id="isfamilypackage"
              onChange={onChangeHandller}
              defaultChecked={specialCheckBox.isfamilypackage}
            />
          </div>
        </Col>
        <hr />
        <Col xxl={7}>
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
            <div className="table-responsive table-card">
              <table
                className="table align-middle table-nowrap"
                id="customerTable"
              >
                <thead className="table-light">
                  <tr>
                    <th className="">S.No.</th>
                    <th className="">City</th>
                    <th className="">Nights</th>
                    <th className="">Remove</th>
                  </tr>
                </thead>
                <tbody>
                  {cityAndNight.length > 0 &&
                    cityAndNight.map((item, index) => {
                      return (
                        <tr key={index}>
                          <td>City {index + 1}</td>
                          <td width={320}>
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
                                (item.nights?.length < 1 || !item.nights) &&
                                showInvalid
                              }
                              // readOnly={readOnly}
                              required
                            />
                            {(item.nights?.length < 1 || !item.nights) &&
                            showInvalid ? (
                              <p style={{ fontSize: "12px", color: "red" }}>
                                Nights is Required
                              </p>
                            ) : (
                              ""
                            )}
                          </td>
                          <td>
                            <i
                              className="ri-close-line"
                              onClick={() => removeCityHandler(item.id)}
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
        <Col xxl={5}>
          <div>
            <label htmlFor="transport" className="form-label">
              Transport
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
        {/**Package inclusions*/}
        <Col xxl={12}>
          <div>
            <label htmlFor="add_city" className="form-label">
              Add Package Inclusions{" "}
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
                Add Package Inclusions
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
                    <th className="">Inclusions</th>
                    <th className="">Remove</th>
                  </tr>
                </thead>
                <tbody>
                  {inclusions.length > 0 &&
                    inclusions.map((item, index) => {
                      return (
                        <tr key={index}>
                          <td> {index + 1}</td>
                          <td>
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
                                Inclusions is Required
                              </p>
                            ) : (
                              ""
                            )}
                          </td>
                          <td>
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
</Modal>;
