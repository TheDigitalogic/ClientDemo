import { GET_DESTINATION_LIST, SAVE_MESSAGE, GET_MESSAGE } from "./actiontype";
const INITIAL_STATE = {
  destinationList: [],
  saveMessage: "",
  getMessage: "",
};
const destinationInformation = (state = INITIAL_STATE, action) => {
  switch (action.type) {
    case GET_DESTINATION_LIST:
      return {
        ...state,
        destinationList: action.payload,
      };
    case SAVE_MESSAGE:
      return {
        ...state,
        saveMessage: action.payload,
      };
    case GET_MESSAGE:
      return {
        ...state,
        getMessage: action.payload,
      };
    default:
      return state;
  }
};
export default destinationInformation;
