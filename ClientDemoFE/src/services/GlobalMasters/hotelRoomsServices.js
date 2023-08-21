import { url } from "../common/const";
import { get, post } from "../common/https";
/**This api for get Hotel Room list*/
export const GetHotelRoomList = () => {
  const getdata = get(`${url}/api/HotelMealPlan/list`);
  return getdata;
};
/**This api for save Hotel Room list*/
export const SaveHotelRoomList = (data) =>
  post(`${url}/api/HotelMealPlan/save`, data);
