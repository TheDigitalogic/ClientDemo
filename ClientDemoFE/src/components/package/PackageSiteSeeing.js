import { Table } from "antd";
import React from "react";
import ButttonTravelNinjaz from "../Common/GloablMaster/ButttonTravelNinjaz";

const PackageSiteSeeing = ({
  dataSource,
  modalSaveHandller,
  cancelHandller,
  toggleArrowTab,
  activeArrowTab,
}) => {
  const columns = [
    {
      title: "Site",
      dataIndex: "site",
    },
    {
      title: "Rate",
      dataIndex: "rate",
      render: (text, record) => {
        return (
          <span>
            {parseInt(record.rate).toLocaleString("en-IN", {
              maximumFractionDigits: 0,
              style: "currency",
              currency: "INR",
            })}
          </span>
        );
      },
    },
  ];
  return (
    <>
      <div>
        <Table columns={columns} dataSource={dataSource} />
      </div>
      <div className="d-flex justify-content-xl-between mt-3">
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

export default PackageSiteSeeing;
