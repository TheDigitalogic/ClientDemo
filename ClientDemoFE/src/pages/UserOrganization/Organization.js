import React, { useEffect, useState } from "react";
import { ToastContainer } from "react-toastify";
import {
  Card,
  CardBody,
  Col,
  Container,
  Modal,
  ModalBody,
  ModalHeader,
  Row,
} from "reactstrap";
import BreadCrumb from "../../components/Common/BreadCrumb";
import ButttonTravelNinjaz from "../../components/Common/GloablMaster/ButttonTravelNinjaz";
import { Table } from "antd";
import "antd/dist/antd.css";
import B2BLabelInput from "../../common/B2BLabelInput";
import {
  errornotify,
  successNotify,
} from "../../components/Common/notification";
import {
  getOrganizationService,
  saveOrganizationService,
} from "../../services/OrganizationService";
import { getSessionUsingSessionStorage } from "../../services/common/session";
const Organization = () => {
  const [organizationList, setOrganizationList] = useState([]);
  const [add_modal_is_open, set_add_modal_is_open] = useState(false);
  const [showInvalid, setShowInvalid] = useState(false);
  const [value, setValue] = useState({
    organizationCode: undefined,
    organizationName: undefined,
    organizationId: 0,
  });

  const { organizationCode, organizationName, organizationId } = value;
  const [editable, setEditable] = useState(false);
  const [userName, setUserName] = useState();
  /**This function for a modal close and open */
  const tog_scroll = () => {
    set_add_modal_is_open(!add_modal_is_open);
  };
  /**onchange handller */
  const onChangeHandller = (e) => {
    setValue({ ...value, [e.target.name]: e.target.value });
  };
  /**This function for open modal close */
  const handleAdd = () => {
    setEditable(false);
    setValue({
      organizationCode: "",
      organizationName: "",
    });
    tog_scroll();
  };
  const getOrganizationFunc = async () => {
    try {
      const userResponse = await getOrganizationService();
      if (userResponse?.data?.status === "SUCCESS") {
        setOrganizationList(userResponse?.data?.data);
      } else {
        throw userResponse?.messsage;
      }
    } catch (error) {
      errornotify("error");
    }
  };
  useEffect(() => {
    getOrganizationFunc();
  }, []);
  /**This is for cancel handller */
  const cancelHandller = () => {
    tog_scroll();
    setShowInvalid(false);
  };
  /**This is for save or update organization details */
  const saveOrganization = async (operation) => {
    try {
      setShowInvalid(true);
      const data = {
        OrganizationId: organizationId,
        OrganizationCode: organizationCode,
        OrganizationName: organizationName,
        Operation: operation,
        Row_created_by: userName,
        Row_altered_by: userName,
      };
      const response = await saveOrganizationService(data);

      if (response?.status === "SUCCESS") {
        setShowInvalid(false);
        successNotify(response?.messsage);
        tog_scroll();
        getOrganizationFunc();
      } else {
        throw response?.messsage;
      }
    } catch (error) {
      debugger;
      errornotify(error);
    }
  };
  /**this is for delete handller */
  const deleteHandller = async (record) => {
    try {
      const response = await saveOrganizationService({
        OrganizationId: record?.organizationId,
        OrganizationCode: record?.organizationCode,
        OrganizationName: record?.organizationName,
        Operation: "DELETE",
        Row_created_by: userName,
        Row_altered_by: userName,
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
  /**This is for update handller */
  const editHandller = async (record) => {
    setEditable(true);
    tog_scroll();
    setValue({
      ...value,
      organizationCode: record?.organizationCode,
      organizationName: record?.organizationName,
      organizationId: record?.organizationId,
    });
  };
  /**get details from local storage */
  useEffect(() => {
    let promise = getSessionUsingSessionStorage();
    promise
      .then(function (value) {
        return value;
      })
      .then((value) => {
        setUserName(value.userName);
      });
  }, []);
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
      title: "Organization Code",
      dataIndex: "organizationCode",
    },
    {
      title: "Organization Name",
      dataIndex: "organizationName",
    },
  ];
  return (
    <>
      <ToastContainer />
      <div className="page-content">
        <Container fluid>
          <BreadCrumb
            title="Organization"
            isSubTitle={false}
            pageTitle="Organization"
          />
          <Row>
            <Col lg={12}>
              <Card>
                <CardBody>
                  <div>
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
                  </div>
                  <div>
                    <Table
                      columns={columns}
                      dataSource={organizationList}
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
          {editable ? "Edit " : "Add"} Organization
        </ModalHeader>
        <ModalBody style={{ minHeight: "20vh" }}>
          <form>
            <div className="row g-3">
              <Col xxl={12}>
                <B2BLabelInput
                  classNameInput={"form-control"}
                  classNameLabel={"form-label"}
                  id={"organizationCode"}
                  labelName={"Organization Code"}
                  value={organizationCode}
                  defaultValue={organizationCode}
                  invalid={!organizationCode && showInvalid}
                  name="organizationCode"
                  onChangeHandller={onChangeHandller}
                  type="text"
                />
                {/* {!organizationCode && showInvalid?<Er} */}
              </Col>
              <Col xxl={12}>
                <B2BLabelInput
                  classNameInput={"form-control"}
                  classNameLabel={"form-label"}
                  id={"organizationName"}
                  labelName={"Organization Name"}
                  value={organizationName}
                  defaultValue={organizationName}
                  invalid={!organizationName && showInvalid}
                  name="organizationName"
                  onChangeHandller={onChangeHandller}
                  type="text"
                />
              </Col>
            </div>
          </form>
        </ModalBody>
        <div className="modal-footer d-flex justify-content-between">
          <div></div>
          <div>
            <ButttonTravelNinjaz
              backGroundColor={"primary"}
              onCLickHancller={() =>
                saveOrganization(editable ? "UPDATE" : "ADD")
              }
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

export default Organization;
