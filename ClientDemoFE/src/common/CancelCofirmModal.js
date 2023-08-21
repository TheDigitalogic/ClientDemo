import React from "react";
import ButttonTravelNinjaz from "../components/Common/GloablMaster/ButttonTravelNinjaz";
import { Modal, ModalBody } from "reactstrap";
import ConfirmationWarning from "../components/Common/GloablMaster/ConfirmationWarning";
const CancelCofirmModal = ({
  modal_standard,
  tog_standard,
  cancelHandller,
}) => {
  return (
    <>
      <Modal
        id="myModal"
        isOpen={modal_standard}
        toggle={() => {
          tog_standard();
        }}
        centered
        style={{ width: "500px" }}
      >
        <ModalBody>
          <ConfirmationWarning />
        </ModalBody>
        <div className="modal-footer gap-1 justify-content-center">
          <ButttonTravelNinjaz
            backGroundColor="primary"
            onCLickHancller={cancelHandller}
            buttonText="Yes"
          />
          <ButttonTravelNinjaz
            backGroundColor="danger"
            onCLickHancller={() => {
              tog_standard();
            }}
            buttonText="Cancel"
          />
        </div>
      </Modal>
    </>
  );
};

export default CancelCofirmModal;
