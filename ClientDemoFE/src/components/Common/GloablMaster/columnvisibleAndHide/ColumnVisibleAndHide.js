import { MoreOutlined } from "@ant-design/icons";
import React from "react";
import {
  DropdownMenu,
  DropdownToggle,
  Input,
  Label,
  UncontrolledDropdown,
} from "reactstrap";

const ColumnVisibleAndHide = ({ changeHandller, switchCheck }) => {
  return (
    <>
      <UncontrolledDropdown
        style={{
          display: "inline-block",
        }}
        className="my-1"
      >
        <DropdownToggle
          tag="button"
          className="btn btn-light mx-1"
          type="button"
        >
          <MoreOutlined style={{ fontSize: "21px" }} />
        </DropdownToggle>
        <DropdownMenu>
          <div className="form-check form-switch form-switch-success mb-3">
            <Input
              className="form-check-input mx-1"
              name="createdBy"
              type="checkbox"
              role="switch"
              id="SwitchCheck1"
              onChange={(e) => changeHandller(e)}
              defaultChecked={!switchCheck.createdBy}
            />
            <Label className="form-check-label" htmlFor="SwitchCheck1">
              Created By
            </Label>
          </div>
          <div className="form-check form-switch form-switch-success mb-3">
            <Input
              className="form-check-input mx-1"
              type="checkbox"
              name="createdDate"
              role="switch"
              id="SwitchCheck2"
              onChange={(e) => changeHandller(e)}
              defaultChecked={!switchCheck.createdDate}
            />
            <Label className="form-check-label" htmlFor="SwitchCheck2">
              Created On
            </Label>
          </div>
          <div className="form-check form-switch form-switch-success mb-3">
            <Input
              className="form-check-input mx-1"
              type="checkbox"
              name="modifiedBy"
              role="switch"
              defaultChecked={!switchCheck.modifiedBy}
              id="SwitchCheck3"
              onChange={(e) => changeHandller(e)}
            />
            <Label className="form-check-label" htmlFor="SwitchCheck3">
              Modified By
            </Label>
          </div>
          <div className="form-check form-switch form-switch-success mb-3">
            <Input
              className="form-check-input mx-1"
              name="modifiedDate"
              type="checkbox"
              role="switch"
              id="SwitchCheck4"
              onChange={(e) => changeHandller(e)}
              defaultChecked={!switchCheck.modifiedDate}
            />
            <Label className="form-check-label" htmlFor="SwitchCheck4">
              Modified On
            </Label>
          </div>
        </DropdownMenu>
      </UncontrolledDropdown>
    </>
  );
};

export default ColumnVisibleAndHide;
