import { message } from "antd";
import React from "react";
import { CSVLink } from "react-csv";
import { DropdownMenu, DropdownToggle, UncontrolledDropdown } from "reactstrap";
import ButttonTravelNinjaz from "../ButttonTravelNinjaz";

const Export = ({ exportedData, downloadExcel, name }) => {
  return (
    <>
      <UncontrolledDropdown style={{ display: "inline-block" }}>
        <DropdownToggle
          tag="button"
          className="btn btn-secondary my-1"
          type="button"
        >
          Export
        </DropdownToggle>
        <DropdownMenu>
          <CSVLink
            filename={`TravelNinjazB2B${name}.csv`}
            data={exportedData}
            className="btn btn-secondary m-1 mx-2"
            onClick={() => {
              message.success("The file is downloading");
            }}
          >
            Export to CSV
          </CSVLink>
          <ButttonTravelNinjaz
            backGroundColor="secondary"
            className="add-btn m-1 mx-1"
            id="cancel-btn"
            onCLickHancller={() => downloadExcel(exportedData, name)}
            buttonText="Export to Excel"
          />
        </DropdownMenu>
      </UncontrolledDropdown>
    </>
  );
};

export default Export;
