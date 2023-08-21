import React, { useState, useEffect } from "react";
import { useSelector } from "react-redux";
import { ReactSession } from "react-client-session";
import logoutImage from "../../assets/images/logout.gif";
import {
  Button,
  Dropdown,
  DropdownItem,
  DropdownMenu,
  DropdownToggle,
  Modal,
  ModalBody,
  ModalHeader,
} from "reactstrap";

//import images
import avatar1 from "../../assets/images/users/avatar-1.jpg";
import { useNavigate } from "react-router-dom";

import {
  getSession,
  getSessionUsingSessionStorage,
  removeLocalStorageItem,
  setSession,
} from "../../services/common/session";
import { changePassword } from "../../services/User/userService";

const ProfileDropdown = () => {
  // const promise = getSession("userDetails");

  const [userName, setUserName] = useState();
  const [fullName, setFullName] = useState();
  const [email, setEmail] = useState();
  const navigate = useNavigate();
  const [modal_center, setmodal_center] = useState(false);
  function tog_center() {
    setmodal_center(!modal_center);
  }

  useEffect(() => {
    let promise = getSessionUsingSessionStorage();
    promise
      .then(function (value) {
        return value;
      })
      .then((value) => {
        setUserName(value.userName);
        setFullName(value.fullName);
        setEmail(value.email);
      });
  }, []);

  /**This function for change password  */
  const passwordChangeFunction = async (email) => {
    if (email) {
      try {
        const response = await changePassword({
          Email: email,
        });
        if (response?.status === "SUCCESS") {
          navigate(
            `/resetpassword?t=${response?.token}&e=${email}&type=changepassword`
          );
        }
      } catch (error) {}
    }
  };
  /**This function for logout handller */
  const logoutHandller = () => {
    //Original working
    //ReactSession.set("userDetails", undefined);

    //New Trial
    // setSession("userDetails", undefined);
    // sessionStorage.clear();
    removeLocalStorageItem("userDetails");
    removeLocalStorageItem("currencySymbol");
    navigate("/login");
    tog_center();
  };
  //Dropdown Toggle
  const [isProfileDropdown, setIsProfileDropdown] = useState(false);
  const toggleProfileDropdown = () => {
    setIsProfileDropdown(!isProfileDropdown);
  };
  return (
    <React.Fragment>
      <Dropdown
        isOpen={isProfileDropdown}
        toggle={toggleProfileDropdown}
        className="ms-sm-3 header-item topbar-user"
      >
        <DropdownToggle tag="button" type="button" className="btn">
          <span className="d-flex align-items-center">
            <img
              className="rounded-circle header-profile-user"
              src={avatar1}
              alt="Header Avatar"
            />
            <span className="text-start ms-xl-2">
              <span className="d-none d-xl-inline-block ms-1 fw-medium user-name-text">
                {fullName}
              </span>
              <span className="d-none d-xl-block ms-1 fs-12 text-muted user-name-sub-text">
                Founder
              </span>
            </span>
          </span>
        </DropdownToggle>
        <DropdownMenu className="dropdown-menu-end">
          <h6 className="dropdown-header">{userName}</h6>
          <DropdownItem href="/profilesettings">
            <i className="mdi mdi-account-circle text-muted fs-16 align-middle me-1"></i>
            <span className="align-middle">Profile</span>
          </DropdownItem>
          {/* <DropdownItem href="#">
            <i className="mdi mdi-message-text-outline text-muted fs-16 align-middle me-1"></i>{" "}
            <span className="align-middle">Messages</span>
          </DropdownItem>
          <DropdownItem href="#">
            <i className="mdi mdi-calendar-check-outline text-muted fs-16 align-middle me-1"></i>{" "}
            <span className="align-middle">Taskboard</span>
          </DropdownItem>
          <DropdownItem href="#">
            <i className="mdi mdi-lifebuoy text-muted fs-16 align-middle me-1"></i>{" "}
            <span className="align-middle">Help</span>
          </DropdownItem> */}
          <div className="dropdown-divider"></div>
          {/* <DropdownItem href="#">
            <i className="mdi mdi-wallet text-muted fs-16 align-middle me-1"></i>{" "}
            <span className="align-middle">
              Balance : <b>$5971.67</b>
            </span>
          </DropdownItem> */}
          {/* <DropdownItem href="#">
            <span className="badge bg-soft-success text-success mt-1 float-end">
              New
            </span>
            <i className="mdi mdi-cog-outline text-muted fs-16 align-middle me-1"></i>{" "}
            <span className="align-middle">Settings</span>
          </DropdownItem> */}
          {/* <DropdownItem href="#">
            <i className="mdi mdi-lock text-muted fs-16 align-middle me-1"></i>{" "}
            <span className="align-middle">Lock screen</span>
          </DropdownItem> */}
          <DropdownItem onClick={() => passwordChangeFunction(email)}>
            <i className="mdi mdi-key text-muted fs-16 align-middle me-1"></i>{" "}
            <span className="align-middle">Change Password</span>
          </DropdownItem>
          <DropdownItem onClick={tog_center}>
            <i className="mdi mdi-logout text-muted fs-16 align-middle me-1"></i>{" "}
            <span className="align-middle" data-key="t-logout">
              Logout
            </span>
          </DropdownItem>
        </DropdownMenu>
      </Dropdown>
      <Modal
        isOpen={modal_center}
        toggle={() => {
          tog_center();
        }}
        centered
      >
        <ModalHeader className="modal-title" />
        <ModalBody className="text-center p-5">
          <img
            src={logoutImage}
            alt="logout"
            style={{ height: "170px", width: "200px" }}
          />
          <div className="mt-4">
            <h4 className="mb-3">Are you sure want to logout ?</h4>
            <div className="hstack gap-2 justify-content-center">
              <Button color="danger" onClick={tog_center}>
                Cancel
              </Button>
              <Button color="primary" onClick={logoutHandller}>
                Yes
              </Button>
            </div>
          </div>
        </ModalBody>
      </Modal>
    </React.Fragment>
  );
};

export default ProfileDropdown;
