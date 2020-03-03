import {writable, readable} from 'svelte/store'


export const route = writable(null);

export const user = readable({name: null}
     , async set => {

        var r = await fetch(`/api/user`);
        set(await r.json());

        return () => {}
     });


     user.subscribe(x => {})