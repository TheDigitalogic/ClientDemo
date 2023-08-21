import { url } from "../common/const";
import { get, post, post_common } from "../common/https";
/**This api for get Package list*/
export const GetPackageList = (params) => {
  const getdata = get(`${url}/api/Package/list?` + params);
  return getdata;
};
/**This api for save Package list*/
export const SavePackageList = (data) => post(`${url}/api/Package/save`, data);

/**This api for delete images */
export const DeletePackageImageList = (data) =>
  post(`${url}/api/Package/DeleteFile`, data);

/**save package images **/
export const savePackageImageList = (data) =>
  post(`${url}/api/Package/SaveFile`, data);
