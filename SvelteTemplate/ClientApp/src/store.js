import { writable, readable } from "svelte/store";

export const route = writable(null);


export const user = readable({ name: null }, set => {
  fetch(`/api/user`).then(async x => set(await x.json()));
  return () => {};
});

export const reglament = readable({}, set => {
   fetch(`/api/user/reglament`).then(async x => set(await x.json()));
   return () => {};
 });
 
//export const reglament = readable({});
// export const reglament = readable({}, async (set) => {
//    let q = await fetch(`/api/reglament`);
//    set(await q.json());
//    return () => {}

// })
