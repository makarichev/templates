<script>
  import { onMount, onDestroy, setContext, getContext } from "svelte";
  import { user, reglament, route} from "./store.js";
  import { asRouterLink } from "./Shared/actions.js";


onMount(async x => {

  let q = await fetch(`/api/user`)
  user.set(await q.json());

  q = await fetch(`/api/user/reglament`)
  reglament.set(await q.json());

})


  user.set();
  reglament.set();
  
  export let components = [];



  
  
  setContext("name", 'Template');
  setContext("filter", null);


</script>

<svelte:head>
  <title>{getContext("name")}</title>
</svelte:head>

{#if components}
  <svelte:component
    this={components[0]}
    components={components.slice(1)}
    />
{/if}




