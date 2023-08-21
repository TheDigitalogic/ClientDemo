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
import { Table } from "antd";
import "antd/dist/antd.css";
import { useState } from "react";
import B2BLabelInput from "../../common/B2BLabelInput";
import {
  deleteUser,
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
import { ToastContainer } from "react-toastify";
const User = () => {
  /**This state for a modal open and false */
  const [add_modal_is_open, set_add_modal_is_open] = useState(false);
  const [userList, setUserList] = useState([]);
  const [editable, setEditable] = useState(false);
  const [oldRole, setOldRole] = useState("");
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
        throw userResponse?.messsage;
      }
    } catch (error) {
      errornotify(error);
    }
  };
  useEffect(() => {
    getUserListFunc();
  }, []);
  /**This function for open modal close */
  const handleAdd = () => {
    setEditable(false);
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
        successNotify(response?.messsage);
        tog_scroll();
        getUserListFunc();
      } else {
        throw response?.messsage;
      }
    } catch (error) {
      errornotify(error);
    }
  };
  /**This is function for edit handller */
  const editHandller = (record) => {
    setEditable(true);
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
  const deleteHandller = async (record) => {
    try {
      const response = await deleteUser({
        FirstName: record?.firstName,
        LastName: record?.lastName,
        Email: record?.email,
        Phone: record?.phone,
        UserName: record?.userName,
        Role: role?.role,
        Operation: "DELETE",
        UserId: record?.userId,
        AspNetUserId: record?.aspNetUserId,
        Is_active: true,
        Role: record?.role,
      });
      if (response?.status === "SUCCESS") {
        successNotify(response?.messsage);
      } else {
        throw response?.messsage;
      }
    } catch (error) {
      errornotify(error);
    }
  };
  /**This is for cancel handller */
  const cancelHandller = () => {
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
              onClick={() => deleteHandller(record)}
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
      <ToastContainer />
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
                  //   invalid={package_name?.length < 1 && showInvalid}
                  name="firstName"
                  onChangeHandller={onChangeHandller}
                  type="text"
                />
              </Col>
              <Col xxl={6}>
                <B2BLabelInput
                  classNameInput={"form-control"}
                  classNameLabel={"form-label"}
                  id={"lastName"}
                  labelName={"Last Name"}
                  value={lastName}
                  defaultValue={lastName}
                  //   invalid={package_name?.length < 1 && showInvalid}
                  name="lastName"
                  onChangeHandller={onChangeHandller}
                  type="text"
                />
              </Col>

              <Col xxl={6}>
                <B2BLabelInput
                  classNameInput={"form-control"}
                  classNameLabel={"form-label"}
                  id={"contactNumber"}
                  labelName={"Contact Number"}
                  value={contactNumber}
                  defaultValue={contactNumber}
                  //   invalid={package_name?.length < 1 && showInvalid}
                  name="contactNumber"
                  onChangeHandller={onChangeHandller}
                  type="number"
                />
              </Col>

              <Col xxl={6}>
                <B2BLabelInput
                  classNameInput={"form-control"}
                  classNameLabel={"form-label"}
                  id={"email"}
                  labelName={"Email"}
                  value={email}
                  defaultValue={email}
                  //   invalid={package_name?.length < 1 && showInvalid}
                  name="email"
                  onChangeHandller={onChangeHandller}
                  type="email"
                />
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
                  //   invalid={package_name?.length < 1 && showInvalid}
                  name="userName"
                  onChangeHandller={onChangeHandller}
                  type="text"
                />
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
                      //   invalid={package_name?.length < 1 && showInvalid}
                      name="password"
                      onChangeHandller={onChangeHandller}
                      type="password"
                    />
                  </Col>
                  <Col xxl={6}>
                    <B2BLabelInput
                      classNameInput={"form-control"}
                      classNameLabel={"form-label"}
                      id={"confirmPassword"}
                      labelName={"Confirm Password"}
                      value={confirmPassword}
                      defaultValue={confirmPassword}
                      //   invalid={package_name?.length < 1 && showInvalid}
                      name="confirmPassword"
                      onChangeHandller={onChangeHandller}
                      type="password"
                    />
                  </Col>
                </>
              ) : (
                ""
              )}
              <Col xxl={6}>
                <div>
                  <Label htmlFor="isRole" className="form-label">
                    Role
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
    </>
  );
};

export default User;
