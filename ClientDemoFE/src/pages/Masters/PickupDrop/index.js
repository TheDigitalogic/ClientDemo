import { Table } from "antd";
import React, { useEffect, useState } from "react";
import { ToastContainer } from "react-toastify";
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
  Tooltip,
} from "reactstrap";
import BreadCrumb from "../../../components/Common/BreadCrumb";
import ButttonTravelNinjaz from "../../../components/Common/GloablMaster/ButttonTravelNinjaz";
import ColumnVisibleAndHide from "../../../components/Common/GloablMaster/columnvisibleAndHide/ColumnVisibleAndHide";
import Export from "../../../components/Common/GloablMaster/export/Export";
import Mastersearch from "../../../components/Common/GloablMaster/Mastersearch";
import Select from "react-select";
import makeAnimated from "react-select/animated";
import {
  downloadExcel,
  filterUniqueDestination,
} from "../../../functions/downloadtoExcel";
import {
  destinationTypes,
  destinationTypesFilter,
} from "../../../components/constants/destinationTypes";
import { GetDestinationList } from "../../../services/GlobalMasters/destinationService";
import {
  errornotify,
  successNotify,
} from "../../../components/Common/notification";
import {
  GetPickupDropList,
  SavePickupDropList,
} from "../../../services/GlobalMasters/pickupAndDropService";
import { appName } from "../../../common/applicationName";
import { getSessionUsingSessionStorage } from "../../../services/common/session";
import { useNavigate } from "react-router-dom";
import { authError } from "../../../services/common/const";
import UseAuth from "../../../components/UseAuth";

const PickupDrop = () => {
  UseAuth();
  // document.title = "PickupDrop | TravelNinjaz B2B";
  document.title = `${appName}-PickupDrop`;
  const [filterColumnWiseDestination, setFilterColumnWiseDestination] =
    useState([]);
  /*Original Api Data**/
  const [dataSource, setDataSource] = useState({
    pickupAndDropList: [],
  });
  const [exportedData, setExportedData] = useState([]);
  const [exportTableData, setExportableData] = useState([]);
  const { pickupAndDropList } = dataSource;
  const navigate = useNavigate();
  /**This state for a check send cityandnights or inclusions send value null or not*/
  const [isNullChild, setIsNullChild] = useState({
    pickupLocationNullable: false,
    dropLocationNullable: false,
  });
  const { pickupLocationNullable, dropLocationNullable } = isNullChild;
  /**Pickup location */
  const [pickupLocation, setPickupLocation] = useState([
    {
      id: 1,
      Pickup_location_name: null,
    },
  ]);

  /**Drop location*/
  const [dropLocation, setDropLocation] = useState([
    {
      id: 1,
      Drop_location_name: null,
    },
  ]);
  const [showInvalid, setShowInvalid] = useState(false);
  const [userName, setUserName] = useState();
  const animatedComponents = makeAnimated();
  const [destinationList, setDestinationList] = useState([]);
  const [pickupDropId, setPickupDropId] = useState(0);
  /**This is option for destination*/
  let [destinationOption, setDestinationOption] = useState([]);
  /**destination selected */
  const [selectedDestinationType, setselectedDestinationType] = useState(null);
  const [selectedDestinationTypeId, setselectedDestinationTypeId] =
    useState(null);
  /**destination name selected*/
  const [selectedDestination, setselectedDestination] = useState(null);
  const [selectedDestinationId, setselectedDestinationId] = useState(null);
  // Tooltip Open state
  const [tooltipOpen, setTooltipOpen] = useState(false);
  const [filterData, setFilterData] = useState([]);
  /**This state for read Only and edit only*/
  const [readOnly, setReadOnly] = useState(false);
  /**This is for over all search */
  const [searchText, setSearchText] = useState("");
  /**This is for hidden/show column*/
  const [switchCheck, setSwitchCheck] = useState({
    createdBy: true,
    createdDate: true,
    modifiedBy: true,
    modifiedDate: true,
  });
  const { createdBy, createdDate, modifiedBy, modifiedDate } = switchCheck;
  const [inputList, setInputList] = useState({
    is_active: true,
  });
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

  /**This function for get list hotel,city and destination */
  const getAllList = async () => {
    try {
      /**destination get api call */
      const destinationResult = await GetDestinationList();
      if (destinationResult?.data?.status === "SUCCESS") {
        setDestinationList(destinationResult.data.data);
      } else {
        throw destinationResult?.message;
      }
      /**Pickupdrop location get api call */
      const pickupDropResult = await GetPickupDropList();
      if (pickupDropResult?.data?.status === "SUCCESS") {
        setDataSource((prevDataSource) => {
          return {
            ...prevDataSource,
            pickupAndDropList: pickupDropResult.data.data,
          };
        });
        let filterDestination = [];
        let unqiueDestiantion = filterUniqueDestination(
          pickupDropResult.data.data
        );
        unqiueDestiantion?.map((item, index) => {
          filterDestination.push({
            text: item?.destination_name,
            value: item?.destination_id,
          });
        });
        setFilterColumnWiseDestination(filterDestination);
      } else {
        throw pickupDropResult?.message;
      }
    } catch (error) {
      errornotify(error);
      // if (error === authError) {
      //   navigate("/login");
      // }
    }
  };
  /**This is for cancel handller */
  const cancelHandller = () => {
    setShowInvalid(false);
    tog_scroll();
    setReadOnly(false);
    setIsNullChild({
      ...isNullChild,
      pickupLocationNullable: false,
      dropLocationNullable: false,
    });
    /**This for mounting call*/
    getAllList();
  };

  /**Edit Handller*/
  const editHandller = (record) => {
    setselectedDestinationType({
      label: record.destination_type_id === 1 ? "Domestic" : "International",
      value: record.destination_type_id,
    });
    setselectedDestinationTypeId(record.destination_type_id);
    setselectedDestination({
      label: record.destination_name,
      value: record.destination_id,
    });
    setPickupDropId(record.pickup_drop_id);
    let pickupDestinationArray = [];
    record?.pickupLocation?.map((item, index) => {
      pickupDestinationArray.push({
        id: index + 1,
        Pickup_location_id: item.pickup_location_id,
        Pickup_location_name: item.pickup_location_name,
      });
    });
    setPickupLocation(pickupDestinationArray);
    let dropDestinationArray = [];
    record?.dropLocation?.map((item, index) => {
      dropDestinationArray.push({
        id: index + 1,
        Drop_location_id: item.drop_location_id,
        Drop_location_name: item.drop_location_name,
      });
    });
    setDropLocation(dropDestinationArray);
    setselectedDestinationId(record.destination_id);
    setInputList({ ...inputList, is_active: record.is_active });
    setReadOnly(true);
    tog_scroll();
  };
  /**Modal save handller */
  const modalSaveHandller = async () => {
    try {
      setShowInvalid(true);
      /**for save pickupLocation List */
      let pickupLocationList = [];
      pickupLocation?.map((item, index) => {
        if (!item.Pickup_location_name) {
          throw "Pickup location is required!";
        }
        pickupLocationList.push({
          Pickup_location_id: item.Pickup_location_id | 0,
          Pickup_location_name: item.Pickup_location_name,
          Is_active: inputList.is_active,
          Row_created_by: userName,
          Row_created_date: new Date(),
          Row_altered_by: userName,
          Row_altered_date: new Date(),
        });
      });
      /**for save droplocation List */
      let dropLocationList = [];
      dropLocation?.map((item, index) => {
        if (!item.Drop_location_name) {
          throw "Drop location is required!";
        }
        dropLocationList.push({
          Drop_location_id: item.Drop_location_id | 0,
          Drop_location_name: item.Drop_location_name,
          Is_active: inputList.is_active,
          Row_created_by: userName,
          Row_created_date: new Date(),
          Row_altered_by: userName,
          Row_altered_date: new Date(),
        });
      });
      if (!selectedDestinationId) {
        throw "Please select Destination";
      }
      if (pickupLocation.length > 0 && dropLocation.length > 0) {
        const pickupAndDropListSave = {
          Pickup_drop_id: pickupDropId,
          Destination_id: selectedDestinationId,
          PickupLocation:
            readOnly && !pickupLocationNullable ? null : pickupLocationList,
          DropLocation:
            readOnly && !dropLocationNullable ? null : dropLocationList,
          Is_active: inputList.is_active,
          Row_created_by: userName,
          Row_created_date: new Date(),
          Row_altered_by: userName,
          Row_altered_date: new Date(),
        };
        const response = await SavePickupDropList(pickupAndDropListSave);
        if (response.data === "SUCCESS") {
          await getAllList();
          successNotify(response?.message);
          tog_scroll();
          setShowInvalid(false);
          setReadOnly(false);
          setIsNullChild({
            ...isNullChild,
            pickupLocationNullable: false,
            dropLocationNullable: false,
          });
        } else {
          throw response?.message;
        }
      } else {
        if (pickupLocation.length < 1) {
          throw "Pickup  location is required.";
        } else if (dropLocation.length < 1) {
          throw "Drop location is required.";
        }
      }
    } catch (error) {
      errornotify(error);
    }
  };
  /**This useEffect  for get list*/
  useEffect(() => {
    getAllList();
  }, []);

  /** addHandller drop location */
  const handleDropLocation = () => {
    setReadOnly(false);
    setIsNullChild({ ...isNullChild, dropLocationNullable: true });
    setDropLocation([
      ...dropLocation,
      {
        id: dropLocation.length + 1,
        Drop_location_name: null,
      },
    ]);
  };
  /**This is function for select destination type handllder */
  const selectDestinationTypeHandller = (selectdestinationType) => {
    setDestinationOption([]);
    setselectedDestination(null);
    setselectedDestinationId(null);
    setselectedDestinationType(selectdestinationType);
    setselectedDestinationTypeId(selectdestinationType.value);
  };
  /**This is function for destination handller*/
  const selectDestinationHandller = (selectdestination) => {
    setselectedDestination(selectdestination);
    setselectedDestinationId(selectdestination.value);
  };
  /**addHandller pickup location */
  const handleAddPickup = () => {
    setReadOnly(false);
    setIsNullChild({ ...isNullChild, pickupLocationNullable: true });
    setPickupLocation([
      ...pickupLocation,
      {
        id: pickupLocation.length + 1,
        Pickup_location_name: null,
      },
    ]);
  };

  /**Remove pickup handller*/
  const removePickupHandller = (id) => {
    setIsNullChild({ ...isNullChild, pickupLocationNullable: true });
    // if (pickupLocation?.length > 1) {
    const filterPickup = pickupLocation.filter((item) => {
      return item.id !== id;
    });
    setPickupLocation(filterPickup);
    // }
    // else {
    //   errornotify("Please Add atleast one pickup location");
    // }
  };
  /**Remove drop handller */
  const removeDropHandller = (id) => {
    setIsNullChild({ ...isNullChild, dropLocationNullable: true });
    // if (dropLocation?.length > 1) {
    const filterDrop = dropLocation.filter((item) => {
      return item.id !== id;
    });
    setDropLocation(filterDrop);
    // } else {
    //   errornotify("Please Add atleast one drop location");
    // }
  };
  /**This state for a modal open and false */
  const [add_modal_is_open, set_add_modal_is_open] = useState(false);
  /**This function for a modal close and open */
  const tog_scroll = () => {
    set_add_modal_is_open(!add_modal_is_open);
  };
  /**This is for export table data to excel*/
  useEffect(() => {
    setExportableData(JSON.parse(JSON.stringify(pickupAndDropList)));
  }, [pickupAndDropList, createdBy, createdDate, modifiedBy, modifiedDate]);
  useEffect(() => {
    if (searchText === "") {
      setExportableData(JSON.parse(JSON.stringify(pickupAndDropList)));
    }
  }, [searchText]);
  /**This is for add button handller*/
  const handleAdd = () => {
    setPickupDropId(0);
    setselectedDestinationType(null);
    setselectedDestinationTypeId(null);
    setselectedDestination(null);
    setselectedDestinationId(null);
    setInputList({ ...inputList, is_active: true });
    setPickupLocation([
      {
        id: 1,
        Pickup_location_name: null,
      },
    ]);
    setDropLocation([
      {
        id: 1,
        Drop_location_name: null,
      },
    ]);
    setReadOnly(false);
    tog_scroll();
  };
  /**input change handller*/
  const onChangeHandller = (e) => {
    if (e.target.name === "is_active") {
      setInputList({ ...inputList, [e.target.name]: e.target.checked });
    } else if (
      e.target.name === "createdBy" ||
      e.target.name === "createdDate" ||
      e.target.name === "modifiedBy" ||
      e.target.name === "modifiedDate"
    ) {
      setSwitchCheck({ ...switchCheck, [e.target.name]: !e.target.checked });
    } else if (e.target.name === "sameaspickup") {
      setIsNullChild({ ...isNullChild, dropLocationNullable: true });
      let newDropLocation = [];
      if (e.target.checked) {
        pickupLocation.map((item, index) => {
          newDropLocation.push({
            id: item.id,
            Drop_location_name: item.Pickup_location_name,
          });
        });
        setDropLocation(newDropLocation);
      } else {
        setDropLocation([
          {
            id: 1,
            Drop_location_name: null,
          },
        ]);
      }
    } else if (e.target.name === "Pickup_location_name") {
      setIsNullChild({ ...isNullChild, pickupLocationNullable: true });
      const pickupLocationList = pickupLocation.map((item, index) => {
        if (parseInt(e.target.id) === parseInt(item.id)) {
          return {
            ...item,
            [e.target.name]: e.target.value,
          };
        } else {
          return item;
        }
      });
      setPickupLocation(pickupLocationList);
    } else if (e.target.name === "Drop_location_name") {
      setIsNullChild({ ...isNullChild, dropLocationNullable: true });
      const dropLocationList = dropLocation.map((item, index) => {
        if (parseInt(e.target.id) === parseInt(item.id)) {
          return {
            ...item,
            [e.target.name]: e.target.value,
          };
        } else {
          return item;
        }
      });
      setDropLocation(dropLocationList);
    }
  };
  /**Over All table search in master */
  const searchInputHandller = (searchValue) => {
    setSearchText(searchValue);
    if (searchText !== "") {
      const filterData = pickupAndDropList.filter((item) => {
        return Object.values(item)
          .join("")
          .toLowerCase()
          .includes(searchText.toLowerCase());
      });
      setFilterData(filterData);
      setExportableData(JSON.parse(JSON.stringify(filterData)));
    } else {
      setFilterData(pickupAndDropList);
      setExportableData(JSON.parse(JSON.stringify(pickupAndDropList)));
    }
  };
  const columns = [
    {
      title: "Action",
      dataIndex: "action",
      width: 80,
      render: (text, record) => {
        return (
          <button
            type="button"
            className="btn btn-sm btn-info"
            onClick={() => editHandller(record)}
          >
            {" "}
            Edit{" "}
          </button>
        );
      },
    },
    {
      title: "Destination Type",
      dataIndex: "destination_type_id",
      width: 190,
      filters: destinationTypesFilter,
      // filterMode: "tree",
      onFilter: (value, record) => record.destination_type_id === value,
      render: (text, record) => {
        return record.destination_type_id === 1 ? "Domestic" : "International";
      },
      sorter: {
        compare: (a, b) => a.destination_type_id - b.destination_type_id,
        multiple: 7,
      },
      defaultSortOrder: "ascend",
      // sorter: (a, b) => a.destination_type_id - b.destination_type_id,
    },
    {
      title: "Destination",
      dataIndex: "destination_name",
      width: 140,
      // ...getColumnSearchProps("destination_name"),
      filters: filterColumnWiseDestination,
      // filterMode: "tree",
      filterSearch: true,
      onFilter: (value, record) => record.destination_id === value,
      sorter: {
        compare: (a, b) => a.destination_name.localeCompare(b.destination_name),
        multiple: 6,
      },
      defaultSortOrder: "ascend",
      // sorter: (a, b) => a.destination_name.localeCompare(b.destination_name),
    },
    {
      title: "Is Active",
      dataIndex: "is_active",
      width: 130,
      filters: [
        {
          text: "Active",
          value: true,
        },
        {
          text: "Inactive",
          value: false,
        },
      ],
      // filterMode: "tree",
      onFilter: (value, record) => record.is_active === value,
      sorter: {
        compare: (a, b) => a.is_active - b.is_active,
        multiple: 5,
      },
      // sorter: (a, b) => a.is_active - b.is_active,
      render: (text, record) => {
        return (
          <div className="form-check form-switch form-switch-success">
            <Input
              className="form-check-input"
              type="checkbox"
              role="switch"
              id="SwitchCheck3"
              onChange={(e) => (record.is_active = e.target.checked)}
              defaultChecked={record.is_active}
              disabled
            />
          </div>
        );
      },
    },
    {
      title: "Created By",
      dataIndex: "row_created_by",
      width: 250,
      sorter: {
        compare: (a, b) => a.row_created_by.localeCompare(b.row_created_by),
        multiple: 4,
      },
      // sorter: (a, b) => a.row_created_by.localeCompare(b.row_created_by),
      hidden: createdBy,
    },
    {
      title: "Created On",
      dataIndex: "row_created_date",
      width: 160,
      sorter: {
        compare: (a, b) => a.row_created_date.localeCompare(b.row_created_date),
        multiple: 3,
      },
      // sorter: (a, b) => a.row_created_date.localeCompare(b.row_created_date),
      hidden: createdDate,
      render: (text, record) => {
        const date = new Date(record.row_created_date);
        return (
          <>
            {date.getDate()}/
            {date.toLocaleString("default", { month: "short" })}/
            {date.getFullYear()}
            {` ${date.getHours()}:${date.getMinutes()}`}
          </>
        );
      },
    },
    {
      title: "Modified By",
      width: 250,
      dataIndex: "row_altered_by",
      sorter: {
        compare: (a, b) => a.row_altered_by.localeCompare(b.row_altered_by),
        multiple: 2,
      },
      // sorter: (a, b) => a.row_altered_by.localeCompare(b.row_altered_by),
      hidden: modifiedBy,
    },
    {
      title: "Modified On",
      dataIndex: "row_altered_date",
      width: 160,
      sorter: {
        compare: (a, b) => a.row_altered_date.localeCompare(b.row_altered_date),
        multiple: 1,
      },
      // sorter: (a, b) => a.row_altered_date.localeCompare(b.row_altered_date),
      hidden: modifiedDate,
      render: (text, record) => {
        const date = new Date(record.row_altered_date);
        return (
          <>
            {date.getDate()}/
            {date.toLocaleString("default", { month: "short" })}/
            {date.getFullYear()}
            {` ${date.getHours()}:${date.getMinutes()}`}
          </>
        );
      },
    },
  ].filter((item) => !item.hidden);
  /**This is for children data*/
  const expandedRowRender = (row) => {
    const pickupLocationColumns = [
      {
        title: "Pickup Location",
        dataIndex: "pickup_location_name",
        render: (text, record) => {
          return <div className="w-100">{record.pickup_location_name}</div>;
        },
      },
    ];
    const dropLocationColumns = [
      {
        title: "Drop Location",
        dataIndex: "drop_location_name",
        render: (text, record) => {
          return <div className="w-100">{record.drop_location_name}</div>;
        },
      },
    ];
    return (
      <div className="d-flex justify-content-between">
        <Table
          columns={pickupLocationColumns}
          dataSource={row.pickupLocation}
          pagination={false}
          className="w-50 mx-2 my-1"
        />
        <Table
          columns={dropLocationColumns}
          dataSource={row.dropLocation}
          pagination={false}
          className="w-50 mx-2 my-1"
        />
      </div>
    );
  };

  /**This is for destination options*/

  useEffect(() => {
    if (destinationList.length > 0 && selectedDestinationTypeId) {
      let tempdestinationOption = [];
      const filterDestinationList = destinationList.filter((item) => {
        return (
          item.destination_type_id === selectedDestinationTypeId &&
          item.is_active
        );
      });
      filterDestinationList.map((item, index) => {
        tempdestinationOption.push({
          label: item.destination_name,
          value: item.destination_id,
        });
      });
      setDestinationOption(tempdestinationOption);
    }
  }, [destinationList, selectedDestinationTypeId]);

  /**set exported Data*/
  let exportArray = [];
  useEffect(() => {
    setExportedData([]);
    if (exportTableData.length > 0) {
      exportTableData?.forEach((elementParent) => {
        const newArray = [
          ...elementParent?.pickupLocation,
          ...elementParent?.dropLocation,
        ];
        newArray?.forEach((elementChild) => {
          exportArray.push({
            Destination_type:
              elementParent.destination_type_id === 1
                ? "Domestic"
                : "International",
            Destination_name: elementParent.destination_name,
            Is_active: elementParent.is_active,
            ...(!createdBy && {
              Row_createdBy: elementParent.row_created_by,
            }),
            ...(!createdDate && {
              Row_createdDate: elementParent.row_created_date,
            }),
            ...(!modifiedBy && {
              Row_modifiedBy: elementParent.row_altered_by,
            }),
            ...(!modifiedDate && {
              Row_modifiedDate: elementParent.row_altered_date,
            }),
            ...(elementChild.pickup_location_name && {
              Pickup_location_name: elementChild.pickup_location_name,
            }),
            ...(elementChild.drop_location_name && {
              Drop_location_name: elementChild.drop_location_name,
            }),
          });
        });
      });
    }
    setExportedData(exportArray);
  }, [exportTableData, filterData]);
  return (
    <>
      <ToastContainer />
      <div className="page-content">
        <Container fluid>
          <BreadCrumb
            title="pickup And Drop"
            isSubTitle={true}
            pageTitle="Masters"
          />
          <Row>
            <Col lg={12}>
              <Card>
                <CardBody>
                  <div id="customerList">
                    <Row className="g-4 mb-3">
                      <Col className="col-sm-auto">
                        <div>
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
                          {/**Export data */}
                          <Export
                            downloadExcel={downloadExcel}
                            exportedData={exportedData}
                            name="PickupAndDropList"
                          />
                          {/**Hide and show column*/}
                          <ColumnVisibleAndHide
                            changeHandller={onChangeHandller}
                            switchCheck={switchCheck}
                          />
                        </div>
                      </Col>
                      {/**This is for search input field */}
                      <Mastersearch inputHandller={searchInputHandller} />
                    </Row>
                  </div>
                  <div>
                    <Table
                      dataSource={
                        searchText?.length > 0 ? filterData : pickupAndDropList
                      }
                      scroll={{
                        // x: 1500,
                        y: 320,
                      }}
                      columns={columns}
                      pagination={{
                        defaultPageSize: 5,
                        showSizeChanger: true,
                        pageSizeOptions: [5, 10, 20, 50, 100],
                        showTotal: (total, range) =>
                          `${range[0]}-${range[1]} of ${total} items  `,
                      }}
                      expandedRowRender={expandedRowRender}
                      bordered
                      locale={{
                        triggerDesc: null,
                        triggerAsc: null,
                        cancelSort: null,
                      }}
                    />
                  </div>
                </CardBody>
              </Card>
            </Col>
          </Row>
        </Container>
      </div>
      {/**This modal for form steps*/}
      {/**Modal */}
      <Modal
        isOpen={add_modal_is_open}
        toggle={() => {
          tog_scroll();
        }}
        centered
        size="xl"
        scrollable={true}
      >
        <ModalHeader
          className="bg-light p-3"
          toggle={() => {
            tog_scroll();
          }}
        >
          {!readOnly ? "Add " : "Edit"} Pickup And Drop
        </ModalHeader>
        <ModalBody style={{ minHeight: "65vh" }}>
          <form>
            <div className="row g-3">
              <Col xxl={6}>
                <div>
                  <Label htmlFor="destinationType" className="form-label">
                    Destination Type
                  </Label>
                  <Select
                    value={selectedDestinationType}
                    id="destinationType"
                    onChange={(chooseOption) => {
                      selectDestinationTypeHandller(chooseOption);
                    }}
                    options={destinationTypes}
                    components={animatedComponents}
                    isDisabled={readOnly}
                  />
                </div>
              </Col>
              <Col xxl={6}>
                <div>
                  <label htmlFor="destination" className="form-label">
                    Destination
                  </label>
                  <Select
                    value={selectedDestination}
                    id="destination"
                    onChange={(chooseOption) => {
                      selectDestinationHandller(chooseOption);
                    }}
                    options={destinationOption}
                    className={
                      !selectedDestination && showInvalid
                        ? "border border-danger"
                        : ""
                    }
                    components={animatedComponents}
                    isDisabled={readOnly}
                  />
                  {!selectedDestination && showInvalid && (
                    <p
                      style={{
                        color: "red",
                        fontSize: "14px",
                        marginLeft: "5px",
                      }}
                    >
                      {" "}
                      Please select Destination
                    </p>
                  )}
                </div>
              </Col>
            </div>
            <hr />
            <div>
              <Row>
                <Col xxl={6}>
                  <div>
                    <label htmlFor="add_city" className="form-label">
                      Pickup Location{" "}
                      <i
                        className="ri-add-line align-bottom mx-2"
                        onClick={handleAddPickup}
                        id="add_city"
                        style={{
                          padding: "3px",
                          fontSize: "14px",
                          borderRadius: "50%",
                          backgroundColor: "#099885",
                          color: "white",
                          cursor: "pointer",
                        }}
                      ></i>
                      <Tooltip
                        isOpen={tooltipOpen}
                        placement="right"
                        target="add_city"
                        toggle={() => {
                          setTooltipOpen(!tooltipOpen);
                        }}
                      >
                        Add Pickup Location
                      </Tooltip>
                    </label>
                    <div className="table-responsive table-card my-2 mx-1">
                      <table
                        className="table align-middle table-nowrap"
                        id="customerTable"
                      >
                        <thead className="table-light">
                          <tr>
                            <th className="">S.No.</th>
                            <th className="">Pickup Location</th>
                            <th className="">Remove</th>
                          </tr>
                        </thead>
                        <tbody>
                          {pickupLocation?.map((item, index) => {
                            return (
                              <tr key={index}>
                                <td>{index + 1} .</td>
                                <td>
                                  <Input
                                    type="text"
                                    className="form-control"
                                    name="Pickup_location_name"
                                    id={item.id}
                                    value={item.Pickup_location_name}
                                    defaultValue={item.Pickup_location_name}
                                    onChange={(e) => onChangeHandller(e)}
                                    invalid={
                                      !item.Pickup_location_name && showInvalid
                                    }
                                    required
                                  />
                                  {!item.Pickup_location_name &&
                                    showInvalid && (
                                      <p style={{ color: "red" }}>
                                        Pickup Location is required
                                      </p>
                                    )}
                                </td>
                                <td>
                                  <i
                                    className="ri-close-line"
                                    onClick={() =>
                                      removePickupHandller(item.id)
                                    }
                                    style={{
                                      fontSize: "25px",
                                      cursor: "pointer",
                                    }}
                                  ></i>
                                </td>
                              </tr>
                            );
                          })}
                        </tbody>
                      </table>
                    </div>
                  </div>
                </Col>
                <Col xxl={6}>
                  <div className="d-flex justify-content-between align-items-center">
                    <label htmlFor="add_city" className="form-label">
                      Drop Location{" "}
                      <i
                        className="ri-add-line align-bottom mx-2"
                        onClick={handleDropLocation}
                        id="add_city"
                        style={{
                          padding: "3px",
                          fontSize: "14px",
                          borderRadius: "50%",
                          backgroundColor: "#099885",
                          color: "white",
                          cursor: "pointer",
                        }}
                      ></i>
                      <Tooltip
                        isOpen={tooltipOpen}
                        placement="right"
                        target="add_city"
                        toggle={() => {
                          setTooltipOpen(!tooltipOpen);
                        }}
                      >
                        Add Drop Location
                      </Tooltip>
                    </label>
                    <div className="form-check form-switch form-switch-success mb-1">
                      <Input
                        className="form-check-input"
                        type="checkbox"
                        role="switch"
                        name="sameaspickup"
                        id="sameaspickup"
                        onChange={(e) => onChangeHandller(e)}
                      />
                      <Label
                        className="form-check-label"
                        htmlFor="sameaspickup"
                      >
                        Same as Pickup location
                      </Label>
                    </div>
                  </div>
                  <div className="table-responsive table-card my-2 mx-1">
                    <table
                      className="table align-middle table-nowrap"
                      id="customerTable"
                    >
                      <thead className="table-light">
                        <tr>
                          <th className="">S.No.</th>
                          <th className="">Drop Location</th>
                          <th className="">Remove</th>
                        </tr>
                      </thead>
                      <tbody>
                        {dropLocation?.map((item, index) => {
                          return (
                            <tr key={index}>
                              <td>{index + 1} .</td>
                              <td>
                                <Input
                                  type="text"
                                  className="form-control"
                                  name="Drop_location_name"
                                  id={item.id}
                                  value={item.Drop_location_name}
                                  defaultValue={item.Drop_location_name}
                                  onChange={(e) => onChangeHandller(e)}
                                  invalid={
                                    !item.Drop_location_name && showInvalid
                                  }
                                  required
                                />
                                {!item.Drop_location_name && showInvalid && (
                                  <p style={{ color: "red" }}>
                                    Drop Location is required
                                  </p>
                                )}
                              </td>
                              <td>
                                <i
                                  className="ri-close-line"
                                  onClick={() => removeDropHandller(item.id)}
                                  style={{
                                    fontSize: "25px",
                                    cursor: "pointer",
                                  }}
                                ></i>
                              </td>
                            </tr>
                          );
                        })}
                      </tbody>
                    </table>
                  </div>
                </Col>
              </Row>
            </div>
          </form>
        </ModalBody>
        <div className="modal-footer d-flex justify-content-between">
          <div className="form-check form-switch form-switch-success my-1">
            <Input
              className="form-check-input"
              type="checkbox"
              role="switch"
              name="is_active"
              id="SwitchCheck3"
              onChange={(e) => onChangeHandller(e)}
              defaultChecked={inputList.is_active}
            />
            <Label className="form-check-label" htmlFor="SwitchCheck3">
              Is Active
            </Label>
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
      </Modal>
    </>
  );
};

export default PickupDrop;
