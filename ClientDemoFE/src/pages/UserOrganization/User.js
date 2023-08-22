import React, { useEffect } from "react";
import {
  Card,
  CardBody,
  Col,
  Container,
  Input,
  Label,
  Modal,
  ModalBody,
  ModalHeader,
  Row,
} from "reactstrap";
import BreadCrumb from "../../components/Common/BreadCrumb";
import ButttonTravelNinjaz from "../../components/Common/GloablMaster/ButttonTravelNinjaz";
import Mastersearch from "../../components/Common/GloablMaster/Mastersearch";
import CancelCofirmModal from "../../common/CancelCofirmModal";
import { Table } from "antd";
import "antd/dist/antd.css";
import { useState } from "react";
import B2BLabelInput from "../../common/B2BLabelInput";
import {
  deleteUserService,
  getUserList,
  signup,
  updateUser,
} from "../../services/User/userService";
import Select from "react-select";
import makeAnimated from "react-select/animated";
import {
  errornotify,
  successNotify,
} from "../../components/Common/notification";

import RequiredError from "../../common/RequiredError";
const User = () => {
  /**This state for a modal open and false */
  const [add_modal_is_open, set_add_modal_is_open] = useState(false);
  const [userList, setUserList] = useState([]);
  const [editable, setEditable] = useState(false);
  const [showInvalid, setShowInvalid] = useState(false);
  const [oldRole, setOldRole] = useState("");
  const [deleteConfirmModal, setDeleteConfirmModal] = useState(false);
  const [deleteRecord, setDeleteRecord] = useState();
  /**This is for role options*/
  const roleOptions = [
    {
      label: "Standard",
      value: "Standard",
    },
    {
      label: "Admin",
      value: "Admin",
    },
  ];

  /**This is for value */
  const [value, setValue] = useState({
    firstName: undefined,
    userId: undefined,
    aspNetUserId: undefined,
    lastName: undefined,
    userName: undefined,
    contactNumber: undefined,
    email: undefined,
    password: undefined,
    confirmPassword: undefined,
    role: {
      label: "Standard",
      value: "Standard",
    },
  });
  const {
    firstName,
    lastName,
    userName,
    contactNumber,
    email,
    role,
    password,
    confirmPassword,
    aspNetUserId,
    userId,
  } = value;

  const animatedComponents = makeAnimated();
  /**This is function for get users list */
  const getUserListFunc = async () => {
    try {
      const userResponse = await getUserList();
      if (userResponse?.data?.status === "SUCCESS") {
        setUserList(userResponse?.data?.data);
      } else {
        throw userResponse?.message;
      }
    } catch (error) {
      errornotify(error);
    }
  };
  useEffect(() => {
    getUserListFunc();
  }, []);
  /**this is toggle function for delete confirmation */
  const togDeleteModal = () => {
    setDeleteConfirmModal(!deleteConfirmModal);
  };
  /**This function for open modal close */
  const handleAdd = () => {
    setEditable(false);
    setShowInvalid(false);
    setValue({
      firstName: "",
      userId: "",
      aspNetUserId: "",
      lastName: "",
      userName: "",
      contactNumber: "",
      email: "",
      password: "",
      confirmPassword: "",
      role: {
        label: "Standard",
        value: "Standard",
      },
    });
    tog_scroll();
  };
  /**onchange handller */
  const onChangeHandller = (e) => {
    setValue({ ...value, [e.target.name]: e.target.value });
  };
  /**This is for set role */
  const selectRoleHandller = (roleSelect) => {
    setValue({ ...value, role: roleSelect });
  };
  /**This function for a modal close and open */
  const tog_scroll = () => {
    set_add_modal_is_open(!add_modal_is_open);
  };

  /**This is for save or update user details */
  const saveUser = async (operation) => {
    try {
      setShowInvalid(true);
      if (!firstName) {
        throw "First name is required";
      }
      if (!lastName) {
        throw "Last name is required";
      }
      if (!contactNumber) {
        throw "Contact number is required";
      }

      if (!email) {
        throw "Email is required";
      }
      if (!userName) {
        throw "User name is required";
      }
      if (!password && operation === "ADD") {
        throw "Password is required";
      }
      if (!confirmPassword && operation === "ADD") {
        throw "Confirm password is required";
      }
      if (!role.value) {
        throw "Role is required";
      }
      const data = {
        FirstName: firstName,
        LastName: lastName,
        Email: email,
        Phone: contactNumber,
        UserName: userName,
        Password: password,
        ConfirmPassword: confirmPassword,
        Role: role?.value,
        Operation: operation,
        UserId: userId || 0,
        AspNetUserId: aspNetUserId || "",
        OldRole: oldRole,
        Is_active: true,
      };
      const response = editable ? await updateUser(data) : await signup(data);

      if (response?.status === "SUCCESS") {
        successNotify(response?.message);
        setShowInvalid(false);
        tog_scroll();
        getUserListFunc();
      } else {
        throw response?.message;
      }
    } catch (error) {
      errornotify(error);
    }
  };
  /**This is function for edit handller */
  const editHandller = (record) => {
    setEditable(true);
    setShowInvalid(false);
    setOldRole(record?.role);
    setValue({
      ...value,
      firstName: record?.firstName,
      lastName: record?.lastName,
      contactNumber: record?.phone,
      email: record?.email,
      role:
        record?.role.toUpperCase() === "ADMIN"
          ? { label: "Admin", value: "Admin" }
          : { label: "Standard", value: "Standard" },
      userName: record?.userName,
      aspNetUserId: record?.aspNetUserId,
      userId: record?.userId,
    });
    tog_scroll();
  };

  /**this is for delete handller */
  const deleteHandller = async () => {
    try {
      const response = await deleteUserService({
        FirstName: deleteRecord?.firstName,
        LastName: deleteRecord?.lastName,
        Email: deleteRecord?.email,
        Phone: deleteRecord?.phone,
        UserName: deleteRecord?.userName,
        Role: deleteRecord?.role,
        Operation: "DELETE",
        UserId: deleteRecord?.userId,
        AspNetUserId: deleteRecord?.aspNetUserId,
        Is_active: true,
        Role: deleteRecord?.role,
      });
      if (response?.status === "SUCCESS") {
        togDeleteModal();
        setDeleteRecord(undefined);
        successNotify(response?.message);
        getUserListFunc();
      } else {
        throw response?.message;
      }
    } catch (error) {
      errornotify(error);
    }
  };

  /**delete tog handller */
  const deleteTogHandller = (record) => {
    togDeleteModal();
    setDeleteRecord(record);
  };
  /**This is for cancel handller */
  const cancelHandller = () => {
    setShowInvalid(false);
    tog_scroll();
  };

  const columns = [
    {
      title: "Action",
      dataIndex: "action",
      width: 150,
      render: (text, record) => {
        return (
          <>
            <button
              type="button"
              className="btn btn-sm btn-info"
              onClick={() => editHandller(record)}
            >
              {" "}
              Edit{" "}
            </button>
            <button
              type="button"
              className="btn btn-sm btn-danger mx-2"
              onClick={() => deleteTogHandller(record)}
            >
              {" "}
              Delete{" "}
            </button>
          </>
        );
      },
    },
    {
      title: "First Name",
      dataIndex: "firstName",
    },
    {
      title: "Last Name",
      dataIndex: "lastName",
    },
    {
      title: "Username",
      dataIndex: "userName",
    },
    {
      title: "Email",
      dataIndex: "email",
    },
    {
      title: "Role",
      dataIndex: "role",
    },
  ];
  return (
    <>
      <div className="page-content">
        <Container fluid>
          <BreadCrumb title="User" isSubTitle={false} pageTitle="User" />
          <Row>
            <Col lg={12}>
              <Card>
                <CardBody>
                  <div>
                    <Row className="g-4 mb-3">
                      <Col className="col-sm-auto">
                        <ButttonTravelNinjaz
                          backGroundColor="success"
                          className="add-btn me-1 my-1"
                          id="create-btn"
                          onCLickHancller={handleAdd}
                          buttonIcon={
                            <i className="ri-add-line align-bottom me-1"></i>
                          }
                          buttonText="Add"
                        />
                      </Col>
                      {/* <Mastersearch /> */}
                    </Row>
                  </div>
                  <div>
                    <Table
                      columns={columns}
                      dataSource={userList}
                      scroll={{
                        // x: 1500,
                        y: 320,
                      }}
                      pagination={{
                        defaultPageSize: 5,
                        showSizeChanger: true,
                        pageSizeOptions: [5, 10, 20, 50, 100],
                        showTotal: (total, range) =>
                          `${range[0]}-${range[1]} of ${total} items  `,
                      }}
                    />
                  </div>
                </CardBody>
              </Card>
            </Col>
          </Row>
        </Container>
      </div>
      <Modal
        isOpen={add_modal_is_open}
        toggle={() => {
          tog_scroll();
        }}
        centered
        size="lg"
        scrollable={true}
      >
        <ModalHeader
          className="bg-light p-3"
          toggle={() => {
            tog_scroll();
          }}
        >
          {editable ? "Edit " : "Add"} User
        </ModalHeader>
        <ModalBody style={{ minHeight: "30vh" }}>
          <form>
            <div className="row g-3">
              <Col xxl={6}>
                <B2BLabelInput
                  classNameInput={"form-control"}
                  classNameLabel={"form-label"}
                  id={"firstName"}
                  labelName={"First Name"}
                  value={firstName}
                  defaultValue={firstName}
                  invalid={!firstName && showInvalid}
                  name="firstName"
                  onChangeHandller={onChangeHandller}
                  type="text"
                  required={true}
                />
                {!firstName && showInvalid ? (
                  <RequiredError errorMessage={"First Name is required."} />
                ) : (
                  ""
                )}
              </Col>
              <Col xxl={6}>
                <B2BLabelInput
                  classNameInput={"form-control"}
                  classNameLabel={"form-label"}
                  id={"lastName"}
                  labelName={"Last Name"}
                  value={lastName}
                  defaultValue={lastName}
                  invalid={!lastName && showInvalid}
                  name="lastName"
                  onChangeHandller={onChangeHandller}
                  type="text"
                  required={true}
                />
                {!lastName && showInvalid ? (
                  <RequiredError errorMessage={"Last Name is required."} />
                ) : (
                  ""
                )}
              </Col>

              <Col xxl={6}>
                <B2BLabelInput
                  classNameInput={"form-control"}
                  classNameLabel={"form-label"}
                  id={"contactNumber"}
                  labelName={"Contact Number"}
                  value={contactNumber}
                  defaultValue={contactNumber}
                  invalid={!contactNumber && showInvalid}
                  name="contactNumber"
                  onChangeHandller={onChangeHandller}
                  type="number"
                  required={true}
                />
                {!contactNumber && showInvalid ? (
                  <RequiredError errorMessage={"Contact number is required."} />
                ) : (
                  ""
                )}
              </Col>

              <Col xxl={6}>
                <B2BLabelInput
                  classNameInput={"form-control"}
                  classNameLabel={"form-label"}
                  id={"email"}
                  labelName={"Email"}
                  value={email}
                  defaultValue={email}
                  invalid={!email && showInvalid}
                  name="email"
                  onChangeHandller={onChangeHandller}
                  type="email"
                  required={true}
                />
                {!email && showInvalid ? (
                  <RequiredError errorMessage={"Email is required."} />
                ) : (
                  ""
                )}
              </Col>
              <Col xxl={6}>
                <B2BLabelInput
                  classNameInput={"form-control"}
                  classNameLabel={"form-label"}
                  id={"userName"}
                  labelName={"Username"}
                  value={userName}
                  defaultValue={userName}
                  disabled={editable}
                  required={true}
                  invalid={!userName && showInvalid}
                  name="userName"
                  onChangeHandller={onChangeHandller}
                  type="text"
                />
                {!userName && showInvalid ? (
                  <RequiredError errorMessage={"Username is required."} />
                ) : (
                  ""
                )}
              </Col>
              {!editable ? (
                <>
                  {" "}
                  <Col xxl={6}>
                    <B2BLabelInput
                      classNameInput={"form-control"}
                      classNameLabel={"form-label"}
                      id={"password"}
                      labelName={"Password"}
                      value={password}
                      defaultValue={password}
                      required={true}
                      invalid={!password && showInvalid}
                      name="password"
                      onChangeHandller={onChangeHandller}
                      type="password"
                    />
                    {!password && showInvalid ? (
                      <RequiredError errorMessage={"Password is required."} />
                    ) : (
                      ""
                    )}
                  </Col>
                  <Col xxl={6}>
                    <B2BLabelInput
                      classNameInput={"form-control"}
                      classNameLabel={"form-label"}
                      id={"confirmPassword"}
                      labelName={"Confirm Password"}
                      value={confirmPassword}
                      defaultValue={confirmPassword}
                      required={true}
                      invalid={!confirmPassword && showInvalid}
                      name="confirmPassword"
                      onChangeHandller={onChangeHandller}
                      type="password"
                    />
                    {!confirmPassword && showInvalid ? (
                      <RequiredError
                        errorMessage={"ConfirmPassword is required."}
                      />
                    ) : (
                      ""
                    )}
                  </Col>
                </>
              ) : (
                ""
              )}
              <Col xxl={6}>
                <div>
                  <Label htmlFor="isRole" className="form-label">
                    Role <span className="text-danger">*</span>
                  </Label>
                  <Select
                    value={role}
                    id="isRole"
                    onChange={(chooseOption) => {
                      selectRoleHandller(chooseOption);
                    }}
                    options={roleOptions}
                    components={animatedComponents}
                  />
                </div>
                {!role.value && showInvalid ? (
                  <RequiredError errorMessage={"Role is required."} />
                ) : (
                  ""
                )}
              </Col>
            </div>
          </form>
        </ModalBody>
        <div className="modal-footer d-flex justify-content-between">
          <div></div>
          <div>
            <ButttonTravelNinjaz
              backGroundColor={"primary"}
              onCLickHancller={() => saveUser(editable ? "UPDATE" : "ADD")}
              buttonText={editable ? "Update" : "Save"}
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
      </Modal>

      <CancelCofirmModal
        cancelHandller={deleteHandller}
        modal_standard={deleteConfirmModal}
        tog_standard={togDeleteModal}
        message={"Are you sure to Delete?"}
      />
    </>
  );
};

export default User;
