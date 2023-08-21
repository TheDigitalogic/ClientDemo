import React, { useEffect } from "react";
import { Card, CardBody, Col, Modal, ModalBody, ModalHeader } from "reactstrap";
import InputControll from "./InputControll";
import ButttonTravelNinjaz from "../../components/Common/GloablMaster/ButttonTravelNinjaz";
import { useState } from "react";
import { getSessionUsingSessionStorage } from "../../services/common/session";
import { saveTravellingCompany } from "../../services/MarketingMenu";
import {
  errornotify,
  successNotify,
} from "../../components/Common/notification";
import { ToastContainer } from "react-toastify";

const EditPoupup = ({
  isOpenMoal,
  tog_scroll,
  content,
  getTravellingCompanyData,
}) => {
  const [updateContent, setUpdateContent] = useState(content);
  console.log("updateContent", updateContent);
  const [userName, setUserName] = useState();
  const onChangeHandller = (e) => {
    setUpdateContent({ ...updateContent, [e.target.name]: e.target.value });
  };
  /** get session details*/

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
  /**This is for save handller */
  const modalSaveHandller = async () => {
    try {
      const updateData = {
        Travelling_company_id: updateContent?.travelling_company_id,
        Company_name: updateContent?.company_name,
        Mobile_1: updateContent?.mobile_1,
        Mobile_2: updateContent?.mobile_2,
        Email_id_1: updateContent?.email_id_1,
        Email_id_2: updateContent?.email_id_2,
        Website: updateContent?.website,
        Address: updateContent?.address,
        City: updateContent?.city,
        State: updateContent?.state,
        Status: updateContent?.status,
        Is_active: updateContent.is_active,
        Row_created_by: userName,
        Row_created_date: new Date(),
        Row_altered_by: userName,
        Row_altered_date: new Date(),
      };
      const response = await saveTravellingCompany(updateData);
      if (response?.status === "SUCCESS") {
        successNotify("Updated Successfully !");
        setUpdateContent();
        tog_scroll();
      } else {
        throw response?.message;
      }
    } catch (error) {
      errornotify(error);
      setUpdateContent();
    }
    console.log(updateContent);
  };
  /**This is for cancel handller */
  const cancelHandller = () => {
    tog_scroll();
    getTravellingCompanyData();
    setUpdateContent(content);
  };
  return (
    <>
      <ToastContainer />
      <Modal
        isOpen={isOpenMoal}
        toggle={() => {
          tog_scroll();
        }}
        centered
        size="xl"
      >
        <ModalHeader
          className="bg-light p-3"
          toggle={() => {
            tog_scroll();
          }}
        >
          Edit Travelling Company
        </ModalHeader>
        <ModalBody>
          <form>
            <div className="row g-3">
              <Col xxl={6}>
                <InputControll
                  labelName={"State"}
                  readOnly={true}
                  defaultValue={updateContent?.state}
                  value={updateContent?.state}
                  name={"state"}
                  onChangeHandller={onChangeHandller}
                />
              </Col>
              <Col xxl={6}>
                <InputControll
                  labelName={"City"}
                  readOnly={true}
                  defaultValue={updateContent?.city}
                  value={updateContent?.city}
                  name={"city"}
                  onChangeHandller={onChangeHandller}
                />
              </Col>
              <Col xxl={6}>
                <InputControll
                  labelName={"Company Name"}
                  readOnly={true}
                  defaultValue={updateContent?.company_name}
                  value={updateContent?.company_name}
                  name={"company_name"}
                  onChangeHandller={onChangeHandller}
                />
              </Col>
              <Col xxl={6}>
                <InputControll
                  labelName={"Status"}
                  defaultValue={updateContent?.status}
                  value={updateContent?.status}
                  name={"status"}
                  onChangeHandller={onChangeHandller}
                />
              </Col>
              <Col xxl={6}>
                <InputControll
                  labelName={"Mobile 1"}
                  defaultValue={updateContent?.mobile_1}
                  value={updateContent?.mobile_1}
                  name={"mobile_1"}
                  onChangeHandller={onChangeHandller}
                />
              </Col>
              <Col xxl={6}>
                <InputControll
                  labelName={"Mobile 2"}
                  defaultValue={updateContent?.mobile_2}
                  value={updateContent?.mobile_2}
                  name={"mobile_2"}
                  onChangeHandller={onChangeHandller}
                />
              </Col>
              <Col xxl={6}>
                <InputControll
                  labelName={"Email Id 1"}
                  defaultValue={updateContent?.email_id_1}
                  value={updateContent?.email_id_1}
                  name={"email_id_1"}
                  onChangeHandller={onChangeHandller}
                />
              </Col>
              <Col xxl={6}>
                <InputControll
                  labelName={"Email Id 2"}
                  defaultValue={updateContent?.email_id_2}
                  value={updateContent?.email_id_2}
                  name={"email_id_2"}
                  onChangeHandller={onChangeHandller}
                />
              </Col>
              <Col xxl={6}>
                <InputControll
                  labelName={"Website"}
                  defaultValue={updateContent?.website}
                  value={updateContent?.website}
                  name={"website"}
                  onChangeHandller={onChangeHandller}
                />
              </Col>
              <Col xxl={6}>
                <InputControll
                  labelName={"Address"}
                  defaultValue={updateContent?.address}
                  value={updateContent?.address}
                  name={"address"}
                  onChangeHandller={onChangeHandller}
                />
              </Col>
            </div>
          </form>
        </ModalBody>
        <div className="modal-footer">
          <ButttonTravelNinjaz
            backGroundColor={"primary"}
            onCLickHancller={modalSaveHandller}
            buttonText={"Save"}
          />
          <ButttonTravelNinjaz
            backGroundColor={"danger"}
            onCLickHancller={cancelHandller}
            buttonText={"Cancel"}
          />
        </div>
      </Modal>
    </>
  );
};

export default EditPoupup;
