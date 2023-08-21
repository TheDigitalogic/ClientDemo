import { url } from "./common/const";
import { get, post, post_common } from "./common/https";
/**This service for save/delete/upaate organization */
export const saveOrganizationService = (data) =>
  post(`${url}/api/Organization/SaveOrganization`, data);
/**This service for get organization */
export const getOrganizationService = () => {
  const getData = get(`${url}/api/Organization/List`);
  return getData;
};
