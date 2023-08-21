import React from "react";
import { Col } from "reactstrap";

const Mastersearch = ({ inputHandller }) => {
  return (
    <>
      <Col className="col-sm">
        <div className="d-flex justify-content-sm-end">
          <div className="search-box ms-2 my-1">
            <input
              type="text"
              className="form-control search"
              placeholder="Search..."
              onChange={(e) => inputHandller(e.target.value)}
            />
            <i className="ri-search-line search-icon"></i>
          </div>
        </div>
      </Col>
    </>
  );
};

export default Mastersearch;
