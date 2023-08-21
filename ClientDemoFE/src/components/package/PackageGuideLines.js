import React from "react";
import { CKEditor } from "@ckeditor/ckeditor5-react";
import ClassicEditor from "@ckeditor/ckeditor5-build-classic";
import { Card, CardBody, CardHeader, Col, Form, Row } from "reactstrap";
import ButttonTravelNinjaz from "../Common/GloablMaster/ButttonTravelNinjaz";
const PackageGuideLines = ({
  onChange,
  toggleArrowTab,
  activeArrowTab,
  modalSaveHandller,
  packageGuideLines,
  cancelHandller,
}) => {
  console.log("packageGuideLines", packageGuideLines);
  return (
    <>
      <Row>
        <Col lg={12}>
          <Card>
            <CardHeader className="align-items-center d-flex">
              <h4 className="card-title mb-0">Important Guidelines</h4>
            </CardHeader>
            <CardBody>
              <Form method="post">
                {/* {packageGuideLines && ( */}
                {packageGuideLines === undefined ? (
                  <CKEditor
                    editor={ClassicEditor}
                    onReady={(editor) => {
                      // You can store the "editor" and use when it is needed.
                    }}
                    onChange={(event, editor) => {
                      onChange(event, editor);
                    }}
                  />
                ) : (
                  <CKEditor
                    editor={ClassicEditor}
                    data={packageGuideLines}
                    onReady={(editor) => {
                      // You can store the "editor" and use when it is needed.
                    }}
                    onChange={(event, editor) => {
                      onChange(event, editor);
                    }}
                  />
                )}
                {/* )} */}
              </Form>
            </CardBody>
          </Card>
        </Col>
      </Row>
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

export default PackageGuideLines;
