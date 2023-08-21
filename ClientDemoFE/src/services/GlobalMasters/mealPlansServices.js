import { url } from "../common/const";
import { get, post } from "../common/https";
/**This api for get hotel list*/
export const GetMealPlansList = () => {
  const getdata = get(`${url}/api/MealPlan/list`);
  return getdata;
};
