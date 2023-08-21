import FeatherIcon from "feather-icons-react/build/FeatherIcon";
import React from "react";
import Dropzone from "react-dropzone";
import { Link } from "react-router-dom";
import { Card, Col, Input, Label, Row } from "reactstrap";
import { url } from "../../services/common/const";
import ButttonTravelNinjaz from "../Common/GloablMaster/ButttonTravelNinjaz";

const PackageImages = ({
  handleAcceptedFiles,
  selectedFiles,
  removeSelectedImages,
  getImageFileNames,
  onImagePrimaryHandller,
  onDeleteHandller,
  toggleArrowTab,
  activeArrowTab,
  modalSaveHandller,
  cancelHandller,
}) => {
  return (
    <>
      {" "}
      <div className="cardItems">
        <Dropzone
          onDrop={(acceptedFiles) => {
            handleAcceptedFiles(acceptedFiles);
          }}
          accept="image/jpeg,image/png,image/gif,image/jpg"
        >
          {({ getRootProps, getInputProps }) => (
            <div className="dropzone dz-clickable dropzonecustom">
              <div className="dz-message needsclick" {...getRootProps()}>
                <div className="mb-3">
                  <i className="display-4 text-muted ri-upload-cloud-2-fill" />
                </div>
                <h5>Drop Images here or click to upload.</h5>
              </div>
            </div>
          )}
        </Dropzone>
        <hr />
        <p>Selected Images</p>
        {selectedFiles.map((f, i) => {
          return (
            <Card
              className="mt-1 mb-0 shadow-none border dz-processing dz-image-preview dz-success dz-complete"
              key={i + "-file"}
            >
              <div className="p-2">
                <Row className="align-items-center">
                  <Col className="col-auto">
                    <img
                      data-dz-thumbnail=""
                      height="80"
                      className="avatar-sm rounded bg-light"
                      alt={f.name}
                      src={f.preview}
                    />
                  </Col>
                  <Col xxl={10}>
                    <Link to="#" className="text-muted font-weight-bold">
                      {f.name}
                    </Link>
                    <p className="mb-0">
                      <strong>{f.formattedSize}</strong>
                    </p>
                  </Col>
                  <Col>
                    <FeatherIcon
                      icon="x"
                      style={{
                        color: "#364574",
                        cursor: "pointer",
                      }}
                      onClick={() => removeSelectedImages(f)}
                    />
                  </Col>
                </Row>
              </div>
            </Card>
          );
        })}
        <hr />
        <div className="list-unstyled mb-0" id="file-previews">
          <p>Saved Images</p>
          {getImageFileNames?.map((item, index) => {
            return (
              <Card
                className="mt-1 mb-0 shadow-none border d-flex dz-processing dz-image-preview dz-success dz-complete"
                key={index + "-file"}
              >
                <div className="p-2">
                  <Row className="align-items-center">
                    <Col className="col-auto" xxl={4}>
                      <img
                        data-dz-thumbnail=""
                        height="80"
                        className="avatar-sm rounded bg-light"
                        alt={"image"}
                        src={`${url}/Images/Package/${item.package_id}/${item.image_name}`}
                      />
                    </Col>
                    {/* <Col xxl={6}>
                      <Link to="#" className="text-muted font-weight-bold">
                        {item?.image_name}
                      </Link>
                    </Col> */}
                    <Col xxl={7}>
                      <div className="form-check form-radio-success mb-1 mx-2">
                        <Input
                          className="form-check-input"
                          type="radio"
                          name="isprimary"
                          id={`isprimary-${index}`}
                          onChange={(e) => onImagePrimaryHandller(e, item)}
                          defaultChecked={item?.is_primary}
                          // defaultChecked
                        />
                        <Label
                          className="form-check-label"
                          htmlFor={`isprimary-${index}`}
                        >
                          Primary Image
                        </Label>
                      </div>
                    </Col>
                    <Col>
                      <FeatherIcon
                        icon="trash-2"
                        style={{
                          color: "#364574",
                          cursor: "pointer",
                        }}
                        onClick={() => onDeleteHandller(item)}
                      />
                    </Col>
                  </Row>
                </div>
              </Card>
            );
          })}
        </div>
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

export default PackageImages;
