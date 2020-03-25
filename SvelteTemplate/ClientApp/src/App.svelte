<script>
  import { onMount, onDestroy, setContext, getContext } from "svelte";
  import { user, reglament, route} from "./store.js";
  import { asRouterLink } from "./Shared/actions.js";


import Toast from './Shared/Toast.svelte'

  
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
	reglament = {$reglament}
	user = {$user}
    />
{/if}

<Toast></Toast>


