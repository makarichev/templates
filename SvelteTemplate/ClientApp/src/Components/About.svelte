
<script>
    import Layout from '../Shared/Layout.svelte'
    import {user, reglament} from '../store.js'
</script>

<Layout>
    <div class="container">
        ABOUT

        <p>
            {JSON.stringify(process.env)}
        </p>

        <p>
            {JSON.stringify($user)}
        </p>

        <p>
            {JSON.stringify($reglament)}
        </p>

    </div>

</Layout>
