<script>
    import {onMount} from 'svelte'
    //export let mol = null;
    export let posts = []

    
    
</script>
<ul class="list-inline">
{#each posts as item}
    <li class="list-inline-item">{item.NAME}</li>
{/each}
</ul> -->
