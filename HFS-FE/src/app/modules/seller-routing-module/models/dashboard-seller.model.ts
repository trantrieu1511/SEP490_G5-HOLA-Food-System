export class DashboardSeller{
  labels: string[];
  datasets: DashboardDataSets[];
  
  orderCount: number;
  orderCountPercent: number;
  amountCount: number;
  amountCountPercent: number;
  soldCount: number;
  soldCountPercent: number;
}

export class DashboardDataSets{
  label: string;
  data: number[];
  fill: boolean;
  borderColor: string;
  tension: number;
}