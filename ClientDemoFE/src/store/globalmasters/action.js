import { GET_DESTINATION_LIST, SAVE_MESSAGE, GET_MESSAGE } from "./actiontype";
export const destinationData = (destination) => ({
  type: GET_DESTINATION_LIST,
  payload: destination,
});
export const savesuccessmessage = (message) => ({
  type: SAVE_MESSAGE,
  payload: message,
});
export const getsuccessmessage = (message) => ({
  type: GET_MESSAGE,
  payload: message,
});
