export interface User {
  name: string;
  email: string;
  password: string;
}
export interface User_VM {
  email: string;
  password: string;
}

export interface Note {
  title: string;
  body: string;
  date?: Date;
}
