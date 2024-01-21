export type Contact = {
  fullName: string;
  email: string;
  phoneNumber: string;
  address: string;
  course: {
    id: string;
    instituteName: string;
    courseName: string;
    category: string;
    deliveryMethod: string;
    language: string;
    location: string;
    startDate: string;
  } | null;
};
