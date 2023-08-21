import { url } from "../common/const";
import { get, post, post_common } from "../common/https";
/**This service for get destination list*/
export const GetDestinationList = () => {
  const getdata = get(`${url}/api/Destination/list`);
  return getdata;
};

/**This service for add destination list */
export const AddDestinationList = (data) =>
  post(`${url}/api/Destination/Add`, data);

/**This service for update destination list */
export const UpdateDestinationList = (data) =>
  post(`${url}/api/Destination/Update`, data);

export const SaveImageDestination = (data) => {
  post(`${url}/api/Destination/SaveFile`, data);
};
export const saveIamgeDestination = (data) =>
  post(`${url}/api/Destination/SaveFile`, data);
// export const GetDestinationList = async (dispatch) => {
//   try {
//     const response = await axios.get(`${url}/api/Destination/list`, {});
//     dispatch(destinationData(response.data));
//     dispatch(getsuccessmessage(response?.message));
//   } catch (error) {
//     console.log(error);
//   }
// };
/**This api for save and update destination list*/
// export const SaveDestinationList = async (destination, operation) => {
//   try {
//     const destination_json = JSON.stringify(destination);
//     const response = await axios.post(`${url}/api/Destination/save`, null, {
//       params: {
//         destination_json,
//         operation,
//       },
//   } catch (error) {
//     console.log(error);
//   }
// };
// export const SaveDestinationList = async (destination, operation) => {
//   const saveData = post(`${url}/api/Destination/save`, destination, operation);
// };
