
<script>
    import Layout from '../Layout.svelte'
    import {user, reglament} from '../store.js'
    import {onMount, onDestroy, setContext, getContext} from 'svelte'
    import Filter from './Mols/Filter.svelte'
    

    
    

</script>




<Layout>
    <div class="container">
        ABOUT

        <p>
            {JSON.stringify($user)}
        </p>

        <p>
            {JSON.stringify($reglament)}
        </p>

        {getContext('name')}

        

    </div>

</Layout>

