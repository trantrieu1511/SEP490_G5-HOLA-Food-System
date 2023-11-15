export class FoodReport {
  foodId: number;
  foodName: string;
  shopName: string;
  reportBy: string
  reportContent: string;
  reportContents: string[] = [];
  createDate: string;
  updateDate: string;
  updateBy: string;
  status: string;
  note: string;
}